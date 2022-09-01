using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : Character
{
    [BoxGroup("Components"), SerializeField] private SplineFollower _splineFollower;
    [BoxGroup("Data"), SerializeField] private PlayerData _data;
    [BoxGroup("Weapon"), SerializeField] private PistolGun _gun;

    private StateMachine _stateMachine;
    private PlayerIdleState _idleState;
    private PlayerAggroState _aggroState;
    private PlayerRunningState _runningState;

    public PistolGun Gun => _gun;
    public SplineFollower SplineFollower => _splineFollower;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void Update() => _stateMachine.CurrentState.Update();

    public void SetIdleState() => _stateMachine.ChangeState(_idleState);

    public void SetAggroState() => _stateMachine.ChangeState(_aggroState);

    public void SetRunningState() => _stateMachine.ChangeState(_runningState);

    public void Setup()
    {
        base.Setup(_data);
        
        InitializeStateMachine();
        
        _gun.Setup(100);
    }

    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();

        _idleState = new PlayerIdleState(this, _animation);
        _aggroState = new PlayerAggroState(this, _animation);
        _runningState = new PlayerRunningState(this, _animation);
        
        SetIdleState();
    }
}