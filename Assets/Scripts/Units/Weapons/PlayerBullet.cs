using UnityEngine;

public class PlayerBullet : ProjectileBase
{
    protected override void OnTriggerEnter(Collider collider)
    {
        var humanoidPart = collider.GetComponent<HumanoidPart>();

        if (humanoidPart)
        {
            humanoidPart.Health?.ChangeHealth(-1);
            humanoidPart.Rigidbody.AddForce(_direction * _impulseForce, ForceMode.Impulse);
            
            base.OnTriggerEnter(collider);
        }
    }
}