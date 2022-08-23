public class IdleState : CharacterState
{
    private Character _character;
    private ICharacterAnimation _animation;
    
    public IdleState(Character character) : base(character)
    {
        _character = character;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}