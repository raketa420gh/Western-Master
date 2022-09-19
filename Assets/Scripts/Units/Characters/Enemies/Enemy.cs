using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterAppearanceChanger))]
[RequireComponent(typeof(PlayerDetector))]

public class Enemy : Character
{
    [BoxGroup("Data"), SerializeField] private EnemyData _data;
    [BoxGroup("Detect Parameters"), SerializeField] private LayerMask _aimLayerMask;
    [BoxGroup("Weapon"), SerializeField] private PistolGun _gun;

    private CharacterAppearanceChanger _appearanceChanger;
    private PlayerDetector _playerDetector;
    private StateMachine _stateMachine;
    private EnemyIdleState _idleState;
    private EnemyAggroState _aggroState;

    public PlayerDetector PlayerDetector => _playerDetector;
    public float AggroDuration => _data.AggroDuration;

    protected override void Awake()
    {
        base.Awake();
        
        _appearanceChanger = GetComponent<CharacterAppearanceChanger>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        InitializeStateMachine();
        Setup(_data);
        _gun.Setup(100);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
    
    public void SetIdleState() => _stateMachine.ChangeState(_idleState);

    public void SetAggroState() => _stateMachine.ChangeState(_aggroState);

    public void ShootToPlayerAfterDelay(float delay) => Invoke(nameof(ShootToPlayer), delay);
    
    public void LookAtOnlyY(Transform target) => transform.LookAt(new Vector3(
        target.position.x, 
        transform.position.y, 
        target.position.z));

    protected override void Setup(CharacterData data)
    {
        base.Setup(data);

        _appearanceChanger.SetRandomAppearance();
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
    }
    
    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();

        _idleState = new EnemyIdleState(this, _animation);
        _aggroState = new EnemyAggroState(this, _animation, _data.AggroType);
        
        SetIdleState();
    }

    private void Shoot(Vector3 direction) => _gun.Shoot(direction.normalized);

    private void ShootToPlayer()
    {
        if (!IsAlive) 
            return;
        
        var directionToPlayer = _playerDetector.GetPlayerPosition() - _centerTransform.position; 
        Shoot(directionToPlayer);
    }
}