using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CommonEnemyAggroState : EnemyState
{
    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerDetector _playerDetector;
    private string _hangTriggerName;
    private CancellationTokenSource _waitAggroCTS;

    public CommonEnemyAggroState(Enemy enemy, 
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

        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        
        _hangTriggerName = _enemy.HangTriggerName;

        if (_hangTriggerName == null)
            SetIdleAggro();
        else
            SetHangAggro();
        
        _waitAggroCTS = new CancellationTokenSource();
        
        WaitAggro(_waitAggroCTS.Token);
    }

    public override void Exit()
    {
        base.Exit();
        
        _animator.SetBool(AnimationParametersNames.Aggro, false);

        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;

        _hangTriggerName = null;
        
        _waitAggroCTS?.Cancel();
    }

    private void SetIdleAggro()
    {
        _enemy.transform.rotation = Quaternion.Euler(Rotations.DefaultRotation);
        _animator.SetBool(AnimationParametersNames.Aggro, true);
        
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        
        RotateToPlayerSide();
    }

    private void RotateToPlayerSide()
    {
        _enemy.Rotate(Quaternion.Euler(_playerDetector
            .GetPlayerTransform()
            .position.x > _enemy.transform.position.x
            ? Rotations.RightRotation
            : Rotations.LeftRotation));
    }

    private void SetHangAggro()
    {        
        if (_hangTriggerName.Equals(AnimationParametersNames.OnFloor))
        {
            SetIdleAggro();
        }
        else if (_hangTriggerName.Equals(AnimationParametersNames.OnCeiling))
        {
            _enemy.transform.rotation = Quaternion.Euler(Rotations.OnCeilingRotation);
            
            _animator.SetBool(AnimationParametersNames.OnLeftWall, true);
        }
        else if (_hangTriggerName.Equals(AnimationParametersNames.OnLeftWall))
        {
            _enemy.transform.rotation = Quaternion.Euler(Rotations.RightRotation);
            
            _animator.SetTrigger(AnimationParametersNames.OnLeftWall);
        }
        else if (_hangTriggerName.Equals(AnimationParametersNames.OnRightWall))
        {
            _enemy.transform.rotation = Quaternion.Euler(Rotations.LeftRotation);
           
            _animator.SetTrigger(AnimationParametersNames.OnRightWall);
        }
    }

    private async UniTask WaitAggro(CancellationToken cancellationToken)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_enemy.AggroDuration), cancellationToken: cancellationToken);

        _enemy.StateMachine.ChangeState(_playerDetector
            .IsSeePlayer() ? _enemy.AttackState : _enemy.AggroState);
    }
}