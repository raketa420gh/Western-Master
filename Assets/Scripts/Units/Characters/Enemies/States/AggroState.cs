using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AggroState : EnemyState
{
    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerDetector _playerDetector;
    private CancellationTokenSource _waitAggroCTS;

    public AggroState(Enemy enemy, 
        Rigidbody rigidbody,
        Animator animator, 
        PlayerDetector playerDetector) : base(enemy)
    {
        _enemy = enemy;
        _rigidbody = rigidbody;
        _animator = animator;
        _playerDetector = playerDetector;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        //_waitAggroCTS = new CancellationTokenSource();
        //WaitAggro(_waitAggroCTS.Token);
    }

    public override void Exit()
    {
        base.Exit();
        
        //_waitAggroCTS?.Cancel();
    }

    private async UniTask WaitAggro(CancellationToken cancellationToken)
    {
        //await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AggroDuration), cancellationToken: cancellationToken);

        //_enemy.StateMachine.ChangeState(_playerDetector
            //.IsSeePlayer() ? _enemy.AttackState : _enemy.AggroState);
    }
}