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
    public float AggroDuration => _aggroDuration;
    public bool IsDead { get; private set; }

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
    
    private void Setup()
    {
        _speed = _data.Speed;
        _aggroDuration = _data.AggroDuration;
        _playerDetector.Setup(_data.ViewRadius);
        _health.Setup(_data.MaxHealth);
        
        InitializeStateMachine();
        
        OnCreated?.Invoke();
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        IdleState = new IdleState(this, _animator, _playerDetector);
        AggroState = new AggroState(this, _rigidbody, _animator, _playerDetector);
        
        StateMachine.ChangeState(IdleState);
    }

    private void HandleDeath()
    {
        _ragdoll.Kill();
        _ragdoll.SetDeathRagdollSettings();

        _collider.enabled = false;

        IsDead = true;
        OnDeath?.Invoke(this);
        
        enabled = false;
    }

    private void OnHealthOver() => HandleDeath();
}