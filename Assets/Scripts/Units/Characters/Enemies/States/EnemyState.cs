public class EnemyState : BaseState
{
    private Enemy _enemy;

    protected EnemyState(Enemy enemy) => _enemy = enemy;
}