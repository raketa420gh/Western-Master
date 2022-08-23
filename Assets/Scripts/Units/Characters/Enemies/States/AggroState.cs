using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AggroState : CharacterState
{
    private Enemy _character;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerDetector _playerDetector;
    private CancellationTokenSource _waitAggroCTS;

    public AggroState(Enemy character, 
        Rigidbody rigidbody,
        Animator animator, 
        PlayerDetector playerDetector) : base(character)
    {
        _character = character;
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