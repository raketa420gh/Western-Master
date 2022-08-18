using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HumanoidRagdoll))]

public class Enemy : MonoBehaviour
{
    public event Action OnCreated;
    public event Action<Enemy> OnDeath;
    
    public StateMachine StateMachine;
    public EnemyState IdleState;
    public EnemyState AggroState;
    public EnemyState AttackState;

    [BoxGroup("Data"), SerializeField] private EnemyData _data;
    [BoxGroup("Detect Parameters"), SerializeField] private Collider _collider;
    [BoxGroup("Detect Parameters"), SerializeField] private Transform _centerTransform;
    [BoxGroup("Detect Parameters"), SerializeField] private LayerMask _aimLayerMask;
    [BoxGroup("Weapons GameObjects"), SerializeField] private GameObject[] _weaponGameobjects;
    [BoxGroup("Weapons GameObjects"), SerializeField] private DeathDealer[] _deathDealers;
    [BoxGroup("Shield Parameters"), SerializeField] private Shield _shield;
    [BoxGroup("Fireball Enemy Parameters"), SerializeField] private GameObject _fireballProjectileFxGo;
    [BoxGroup("Fireball Enemy Parameters"), SerializeField] private GameObject _fireTranformFxGo;
    [BoxGroup("Fireball Enemy Parameters"), SerializeField] private GameObject[] _mainMeshObjects;

    private EnemyType _type;
    private float _speed = 2;
    private float _aggroDuration = 0.5f;
    private float _distanceToWallDetect = 1f;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerDetector _playerDetector;
    private Health _health;
    private HumanoidRagdoll _ragdoll;
    private string _hangAnimationTriggerName;

    public Transform CenterTransform => _centerTransform;
    public float Speed => _speed;
    public float AggroDuration => _aggroDuration;
    public bool IsDead { get; private set; }

    public string HangTriggerName => _hangAnimationTriggerName;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _playerDetector = GetComponent<PlayerDetector>();
        _health = GetComponent<Health>();
        _ragdoll = GetComponent<HumanoidRagdoll>();
    }

    private void OnEnable() => _health.OnOver += OnHealthOver;

    private void OnDisable() => _health.OnOver -= OnHealthOver;

    private void Start() => Setup();

    private void Update() => StateMachine.CurrentState.Update();

    private void FixedUpdate() => StateMachine.CurrentState.FixedUpdate();

    public void LookAt(Transform target) => transform.LookAt(target);

    public void Rotate(Quaternion rotation) => transform.rotation = rotation;

    public void ToggleImmortal(bool isActive) => _health.ToggleImmortal(isActive);

    public void SetHangAnimationTriggerName(string triggerName) => _hangAnimationTriggerName = triggerName;

    public void TransformToFireball()
    {
        foreach (var obj in _mainMeshObjects)
            obj.SetActive(false);

        if(_fireballProjectileFxGo)
            _fireballProjectileFxGo.SetActive(true);
    }

    public void ToggleFireTransformFX(bool isActive)
    {
        if (_fireTranformFxGo)
        {
            _fireTranformFxGo.SetActive(isActive);
            
            if (isActive)
                _fireTranformFxGo.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    public void TransformToHuman()
    {
        foreach (var obj in _mainMeshObjects)
            obj.SetActive(true);
        
        if(_fireballProjectileFxGo)
            _fireballProjectileFxGo.SetActive(false);
    }

    public void ToggleShieldActivation(bool isActive)
    {
        if (!_shield) 
            return;
        
        _shield.ToggleActivation(isActive);
    }

    private void DropWeapon()
    {
        if (_weaponGameobjects == null)
            return;

        foreach (var weaponGameObject in _weaponGameobjects)
        {
            weaponGameObject.transform.SetParent(null);
            weaponGameObject.AddComponent<Rigidbody>();
        }
    }

    private void DisableDeathDealers()
    {
        foreach (var deathDealer in _deathDealers)
            deathDealer.gameObject.SetActive(false);
    }

    private void Setup()
    {
        _type = _data.Type;
        _speed = _data.Speed;
        _aggroDuration = _data.AggroDuration;
        _distanceToWallDetect = _data.DistanceToWallDetect;
        _playerDetector.Setup(_data.ViewRadius);
        _health.Setup(_data.MaxHealth);
        
        InitializeStateMachine(_type);
        
        OnCreated?.Invoke();
    }

    private void InitializeStateMachine(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Common:
                StateMachine = new StateMachine();
                IdleState = new CommonEnemyIdleState(this, _animator, _playerDetector);
                AggroState = new CommonEnemyAggroState(this, _rigidbody, _animator, _playerDetector);
                AttackState = new CommonEnemyAttackState(this, _rigidbody, _animator, _playerDetector, 
                    _aimLayerMask, _distanceToWallDetect);

                StateMachine.ChangeState(IdleState);
                break;
            case EnemyType.Fire:
                StateMachine = new StateMachine();
                IdleState = new FireEnemyIdleState(this, _animator, _playerDetector);
                AggroState = new FireEnemyAggroState(this, _rigidbody, _animator, _playerDetector);
                AttackState = new FireEnemyAttackState(this, _rigidbody, _playerDetector, 
                    _aimLayerMask, _distanceToWallDetect);

                StateMachine.ChangeState(IdleState);
                break;
        }
    }

    private void HandleDeath()
    {
        _ragdoll.Kill();
        _ragdoll.SetDeathRagdollSettings();

        _collider.enabled = false;

        DropWeapon();
        DisableDeathDealers();
        TransformToHuman();
        ToggleShieldActivation(false);

        IsDead = true;
        OnDeath?.Invoke(this);
        
        enabled = false;
    }

    private void OnHealthOver() => HandleDeath();
}