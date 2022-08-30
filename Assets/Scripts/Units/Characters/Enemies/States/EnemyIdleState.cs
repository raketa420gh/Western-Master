public class EnemyIdleState : EnemyState
{
    private Enemy _enemy;
    private ICharacterAnimation _animation;
    
    public EnemyIdleState(Enemy enemy, ICharacterAnimation animation) : base(enemy)
    {
        _enemy = enemy;
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
    }
}