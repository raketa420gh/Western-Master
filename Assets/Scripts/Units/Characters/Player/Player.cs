using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]

public class Player : Character
{
    [BoxGroup("Data"), SerializeField] private PlayerData _data;
    [BoxGroup("Weapon"), SerializeField] private PistolGun _gun;

    private StateMachine _stateMachine;
    private PlayerIdleState _idleState;
    private PlayerAimingState _aimingState;
    private PlayerRunningState _runningState;
    private ICharacterAnimation _animation;

    public PistolGun Gun => _gun;

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

    private void Start() => Setup();

    private void Update()
    {
        _stateMachine.CurrentState.Update();
        
        if (Input.GetKeyDown(KeyCode.I))
            SetIdleState();
        
        if (Input.GetKeyDown(KeyCode.A))
            SetAimingState();
    }

    private void SetIdleState() => _stateMachine.ChangeState(_idleState);

    private void SetAimingState() => _stateMachine.ChangeState(_aimingState);

    private void SetRunningState() => _stateMachine.ChangeState(_runningState);

    private void Setup()
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