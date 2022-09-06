using System;
using UnityEngine;

public class PlayerAggroState : PlayerState
{
    private Player _player;
    private ICharacterAnimation _animation;
    private Camera _camera = Camera.main;
    private Vector3 _aimingPoint;

    public PlayerAggroState(Player player, ICharacterAnimation animation) : base(player)
    {
        _player = player;
        _animation = animation;
    }

    public override void Enter()
    {
        base.Enter();
        
        _animation.PlayAim();
    }

    public override void Update()
    {
        base.Update();
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.direction * Single.PositiveInfinity, Color.red);

        if (!Physics.Raycast(ray, out RaycastHit hitInfo)) 
            return;
            
        _aimingPoint = hitInfo.point;

        if (Input.GetMouseButtonDown(0))
        {
            var aimingDirection = _aimingPoint - _player.Gun.Muzzle.position;
            _player.Gun.Shoot(aimingDirection);
            
            //_player.transform.LookAt(hitInfo.point);
            //_player.transform.Rotate(Vector3.up, 30);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        _animation.StopAim();
    }
}