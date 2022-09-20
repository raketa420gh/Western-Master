using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(HumanoidRagdoll))]
[RequireComponent(typeof(Health))]


public abstract class Character : MonoBehaviour
{
    public event Action<Character> OnCreated;
    public event Action<Character> OnDeath;
    
    [BoxGroup("Transform"), SerializeField] protected Transform _centerTransform;
    [BoxGroup("Components"), SerializeField] protected CharacterMovement _movement;
    [BoxGroup("Components"), SerializeField] protected HumanoidRagdoll _ragdoll;
    [BoxGroup("Components"), SerializeField] protected ICharacterAnimation _animation;
    [BoxGroup("Components"), SerializeField] protected IHealth _health;
    
    public Transform CenterTransform => _centerTransform;
    public bool IsAlive => _health.Current > 0;

    protected virtual void Awake()
    {
        _animation = GetComponent<ICharacterAnimation>();
        _health = GetComponent<IHealth>();
    }

    protected virtual void OnEnable() => _health.OnOver += OnHealthOver;

    protected virtual void OnDisable() => _health.OnOver -= OnHealthOver;

    public void LookAtOnlyY(Transform target) => transform.LookAt(new Vector3(
        target.position.x, 
        transform.position.y, 
        target.position.z));

    protected virtual void Setup(CharacterData data)
    {
        _movement.Setup(data);
        _health.Setup(data);
    }

    protected virtual void HandleDeath()
    {
        _ragdoll.Kill();

        OnDeath?.Invoke(this);

        enabled = false;
    }

    private void OnHealthOver() => HandleDeath();
}