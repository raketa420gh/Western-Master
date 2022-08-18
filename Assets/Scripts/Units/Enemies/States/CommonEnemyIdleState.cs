using UnityEngine;

public class CommonEnemyIdleState : EnemyState
{
    private Enemy _enemy;
    private Animator _animator;
    private PlayerDetector _playerDetector;
    
    public CommonEnemyIdleState(Enemy enemy, 
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
        
        _animator.SetBool(AnimationParametersNames.Idle, true);
        
        _enemy.transform.rotation = Quaternion.Euler(Rotations.DefaultRotation);
    }

    public override void Update()
    {
        base.Update();
        
        if (_playerDetector.IsSeePlayer())
            StateMachine.ChangeState(_enemy.AggroState);
    }

    public override void Exit()
    {
        base.Exit();
        
        _animator.SetBool(AnimationParametersNames.Idle, false);
    }
}