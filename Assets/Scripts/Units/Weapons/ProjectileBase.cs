using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected float _impulseForce = 110f;

    protected Vector3 _direction = Vector3.forward;

    private void FixedUpdate() => _rigidbody.transform.Translate(_direction * _speed);

    protected virtual void OnTriggerEnter(Collider other) => Destroy(gameObject);

    public void SetDirection(Vector3 direction) => _direction = direction;
}