using UnityEngine;

public class EnemyBullet : ProjectileBase
{
    protected override void OnTriggerEnter(Collider collider)
    {
        var humanoidPart = collider.GetComponent<HumanoidPart>();

        if (humanoidPart)
        {
            humanoidPart.Rigidbody.AddForce(_direction * _impulseForce, ForceMode.Impulse);
            humanoidPart.Health?.ChangeHealth(-1);
            
            base.OnTriggerEnter(collider);
        }
    }
}