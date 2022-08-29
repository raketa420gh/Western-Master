using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]

public class Player : Character
{
    [BoxGroup("Components"), SerializeField] private SplineFollower _splineFollower;
    [BoxGroup("Data"), SerializeField] private PlayerData _data;
    [BoxGroup("Weapon"), SerializeField] private PistolGun _gun;

    private StateMachine _stateMachine;
    private PlayerIdleState _idleState;
    private PlayerAimingState _aimingState;
    private PlayerRunningState _runningState;
    private ICharacterAnimation _animation;

    public PistolGun Gun => _gun;
    public SplineFollower SplineFollower => _splineFollower;

    protected override void Awake()
    {
        base.Awake();
        
        _animation = GetComponent<ICharacterAnimation>();
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

    public void SetAimingState() => _stateMachine.ChangeState(_aimingState);

    public void SetRunningState() => _stateMachine.ChangeState(_runningState);

    public void Setup()
    {
        base.Setup(_data);
        
        InitializeStateMachine();
        
        _gun.Setup(10);
    }

    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();

        _idleState = new PlayerIdleState(this, _animation);
        _aimingState = new PlayerAimingState(this, _animation);
        _runningState = new PlayerRunningState(this, _animation);
        
        SetIdleState();
    }
}