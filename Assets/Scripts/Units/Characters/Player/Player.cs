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

    private void Awake()
    {
        _animation = GetComponent<ICharacterAnimation>();
    }

    private void Start() => Setup();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SetIdleState();
        
        if (Input.GetKeyDown(KeyCode.A))
            SetAimingState();

        if (Input.GetKeyDown(KeyCode.S))
        {
            var enemy = FindObjectOfType<Enemy>();
            var direction = enemy.CenterTransform.position - transform.position;
            _gun.Shoot(direction);
        }
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
        
        SetIdleState();
    }
}