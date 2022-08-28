using UnityEngine;

public class PlayerRunningState : PlayerState
{
    private Player _player;
    private ICharacterAnimation _animation;
    private Vector3 _aimingPoint;

    public PlayerRunningState(Player player, ICharacterAnimation animation) : base(player)
    {
        _player = player;
        _animation = animation;
    }

    public override void Enter()
    {
        base.Enter();
        
        _animation.PlayRun();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        
        _animation.StopRun();
    }
}