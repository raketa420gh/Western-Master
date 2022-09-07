public class EnemyAggroState : EnemyState
{
    private Enemy _enemy;
    private ICharacterAnimation _animation;
    private EnemyAggroType _aggroType;
    
    public EnemyAggroState(Enemy enemy, ICharacterAnimation animation, EnemyAggroType aggroType) : base(enemy)
    {
        _enemy = enemy;
        _animation = animation;
        _aggroType = aggroType;
    }
    
    public override void Enter()
    {
        base.Enter();

        var playerPosition = _enemy.PlayerDetector.GetPlayerPosition();

        switch (_aggroType)
        {
            case EnemyAggroType.Simply:
            {
                _enemy.transform.LookAt(playerPosition);
                _animation.PlayAim();
                _enemy.ShootForwardWithDelay(_enemy.AggroDuration);
                break;
            }
            case EnemyAggroType.WithMoving:
            {
                //MoveTo
                _enemy.transform.LookAt(playerPosition);
                _animation.PlayAim();
                _enemy.ShootForwardWithDelay(_enemy.AggroDuration);
                break;
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        
        _animation.StopAim();
    }
}