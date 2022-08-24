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

    public void SetIdleState() => _stateMachine.ChangeState(_idleState);

    public void SetAimingState() => _stateMachine.ChangeState(_aimingState);

    private void Setup()
    {
        base.Setup(_data);
        
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();

        _idleState = new PlayerIdleState(this, _animation);
        _aimingState = new PlayerAimingState(this, _animation);
        
        SetAimingState();
    }
}