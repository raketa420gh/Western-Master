using UnityEngine;

public class CommonEnemyAttackState : EnemyState
{
    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerDetector _playerDetector;
    private LayerMask _aimLayerMask;
    private Vector3 _targetPosition;
    private float _distanceToWall;

    public CommonEnemyAttackState(Enemy enemy, 
        Rigidbody rigidbody, 
        Animator animator,
        PlayerDetector playerDetector,
        LayerMask aimLayerMask, 
        float distanceToWall) : base(enemy)
    {
        _enemy = enemy;
        _rigidbody = rigidbody;
        _animator = animator;
        _playerDetector = playerDetector;
        _aimLayerMask = aimLayerMask;
        _distanceToWall = distanceToWall;
    }

    public override void Enter()
    {
        base.Enter();
        
        _rigidbody.useGravity = false;
        
        Aim();
        
        _enemy.LookAt(_playerDetector.GetPlayerTransform());

        _animator.SetTrigger(AnimationParametersNames.Attack);

        _enemy.ToggleShieldActivation(true);
    }

    public override void FixedUpdate()
    {
        base.Update();
        
        if (Vector3.Distance(_rigidbody.position, _targetPosition) > _distanceToWall)
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, _targetPosition, 
                _enemy.Speed * Time.timeScale);
        else
            _enemy.StateMachine.ChangeState(_enemy.AggroState);
    }

    public override void Exit()
    {
        base.Exit();

        _rigidbody.useGravity = true;
        
        _enemy.ToggleShieldActivation(false);
    }

    private void Aim()
    {
        var position = _enemy.CenterTransform.position;
        var rayDirection = _playerDetector.GetPlayerTransform().position - position;

        RaycastHit hit;
        
        var isSeeObstacle = Physics.Raycast(position, 
            rayDirection, out hit, 1000, _aimLayerMask);

        SetNextAggroStateAnimationTrigger(hit);

        if (isSeeObstacle)
            _targetPosition = hit.point;
    }

    private void SetNextAggroStateAnimationTrigger(RaycastHit hit)
    {
        if (hit.collider.CompareTag(TagNames.RightWall))
            _enemy.SetHangAnimationTriggerName(AnimationParametersNames.OnRightWall);

        if (hit.collider.CompareTag(TagNames.LeftWall))
            _enemy.SetHangAnimationTriggerName(AnimationParametersNames.OnLeftWall);

        if (hit.collider.CompareTag(TagNames.Floor))
            _enemy.SetHangAnimationTriggerName(AnimationParametersNames.OnFloor);

        if (hit.collider.CompareTag(TagNames.Ceiling))
            _enemy.SetHangAnimationTriggerName(AnimationParametersNames.OnCeiling);
    }
}