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

        switch (_aggroType)
        {
            case EnemyAggroType.Simply:
            {
                //Rotate
                _animation.PlayAim();
                break;
            }
            case EnemyAggroType.WithMoving:
            {
                //MoveTo
                //Rotate
                _animation.PlayAim();
                break;
            }
            case EnemyAggroType.Trick:
            {
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
    }
}