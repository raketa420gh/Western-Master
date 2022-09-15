using System;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;

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

        _player.SplineFollower.enabled = false;
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

            //КОСТЫЛЬ. УБРАТЬ. СРОЧНО!
            var lookPoint = Object.Instantiate(new GameObject(), hitInfo.point, Quaternion.identity);
            //
            
            _player.LookAtOnlyY(lookPoint.transform);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _player.SplineFollower.enabled = true;
        _animation.StopAim();
    }
}