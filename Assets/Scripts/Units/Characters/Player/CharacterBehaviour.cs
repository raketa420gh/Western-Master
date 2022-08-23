public class CharacterBehaviour
{
    private Character _character;
    private StateMachine _stateMachine;
    private IdleState _idleState;
    private AimingState _aimingState;

    public CharacterBehaviour(Character character)
    {
        _character = character;
    }

    public void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();
        _idleState = new IdleState(_character);
        _aimingState = new AimingState(_character);

        _stateMachine.ChangeState(_idleState);
    }
}