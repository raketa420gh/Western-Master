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
        var playerTransform = _enemy.PlayerDetector.GetTransform();

        switch (_aggroType)
        {
            case EnemyAggroType.Simply:
            {
                _enemy.LookAtOnlyY(playerTransform);
                _animation.PlayAim();
                _enemy.ShootToPlayerAfterDelay(_enemy.AggroDuration);
                break;
            }
            case EnemyAggroType.WithMoving:
            {
                //MoveTo
                _enemy.LookAtOnlyY(playerTransform);
                _animation.PlayAim();
                _enemy.ShootToPlayerAfterDelay(_enemy.AggroDuration);
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