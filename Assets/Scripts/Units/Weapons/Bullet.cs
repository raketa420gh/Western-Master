using UnityEngine;

public class Bullet : ProjectileBase
{
    protected override void OnTriggerEnter(Collider collider)
    {
        var rigidbody = collider.GetComponent<Rigidbody>();
        
        if (rigidbody)
            rigidbody.AddForce(_rigidbody.velocity * _impulseForce, ForceMode.Impulse);

        var enemy = collider.GetComponent<Enemy>();
        
        if (enemy)
            enemy.Death();

        base.OnTriggerEnter(collider);
    }
}