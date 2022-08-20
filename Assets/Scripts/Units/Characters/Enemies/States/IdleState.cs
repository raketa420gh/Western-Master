using UnityEngine;

public class IdleState : EnemyState
{
    private Enemy _enemy;
    private Animator _animator;
    private PlayerDetector _playerDetector;
    
    public IdleState(Enemy enemy, 
        Animator animator, 
        PlayerDetector playerDetector) : base(enemy)
    {
        _enemy = enemy;
        _animator = animator;
        _playerDetector = playerDetector;
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