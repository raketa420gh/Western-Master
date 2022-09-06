public class PlayerIdleState : PlayerState
{
    private Player _player;
    private ICharacterAnimation _animation;
    
    public PlayerIdleState(Player player, ICharacterAnimation animation) : base(player)
    {
        _player = player;
        _animation = animation;
    }

    public override void Enter()
    {
        base.Enter();
        
        _animation.PlayIdle();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        
        _animation.StopIdle();
    }
}