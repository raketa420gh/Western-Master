using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterAppearanceChanger))]

public class Enemy : Character
{
    [BoxGroup("Data"), SerializeField] private EnemyData _data;

    [BoxGroup("Detect Parameters"), SerializeField]
    private LayerMask _aimLayerMask;

    private CharacterAppearanceChanger _appearanceChanger;
    private PlayerDetector _playerDetector;
    
    private StateMachine _stateMachine;
    private EnemyIdleState _idleState;
    private EnemyAggroState _aggroState;

    protected override void Awake()
    {
        base.Awake();
        
        _appearanceChanger = GetComponent<CharacterAppearanceChanger>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        Setup(_data);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
    
    public void SetIdleState() => _stateMachine.ChangeState(_idleState);

    public void SetAimingState() => _stateMachine.ChangeState(_aggroState);

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
        _aggroState = new EnemyAggroState(this, _animation);
        
        SetIdleState();
    }
}