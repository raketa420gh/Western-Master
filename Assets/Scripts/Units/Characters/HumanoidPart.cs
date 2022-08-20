using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class HumanoidPart : SerializedMonoBehaviour
{
    [OdinSerialize] protected IHealth _health;

    [SerializeField] protected Rigidbody _rigidbody;

    public IHealth Health => _health;

    public Rigidbody Rigidbody => _rigidbody;


#if UNITY_EDITOR

    [Button]
    private void FindRigidBody() => _rigidbody = GetComponent<Rigidbody>();

#endif
}