public class PlayerVictoryState : PlayerState
{
    private Player _player;
    private ICharacterAnimation _animation;
    
    public PlayerVictoryState(Player player, ICharacterAnimation animation) : base(player)
    {
        _player = player;
        _animation = animation;
    }

    public override void Enter()
    {
        base.Enter();
        
        _animation.PlayDance();
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