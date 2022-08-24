using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _speed = 50f;
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] protected float _impulseForce = 110f;

    private Vector3 _direction = Vector3.forward;

    public float ImpulseForce => _impulseForce;

    private void FixedUpdate()
    {
        _rigidbody.transform.Translate(_direction * (_speed * Time.fixedDeltaTime));
    }

    protected virtual void OnTriggerEnter(Collider other) => Destroy(gameObject);

    public void SetDirection(Vector3 direction) => _direction = direction;
}