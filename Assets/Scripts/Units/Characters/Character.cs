using System;
using DG.Tweening;
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

    [BoxGroup("Transform"), SerializeField]
    protected Transform _centerTransform;

    [BoxGroup("Components"), SerializeField]
    protected CharacterMovement _movement;

    [BoxGroup("Components"), SerializeField]
    protected HumanoidRagdoll _ragdoll;

    [BoxGroup("Components")] protected ICharacterAnimation _animation;
    [BoxGroup("Components")] protected IHealth _health;

    public Transform CenterTransform => _centerTransform;
    public bool IsAlive => _health.Current > 0;

    protected virtual void Awake()
    {
        _animation = GetComponent<ICharacterAnimation>();
        _health = GetComponent<IHealth>();
    }

    protected virtual void OnEnable() => _health.OnOver += OnHealthOver;

    protected virtual void OnDisable() => _health.OnOver -= OnHealthOver;

    public void LookAtSmoothOnlyY(Transform target, float duration)
    {
        var towards = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.DOLookAt(towards, duration);
    }

    protected virtual void Setup(CharacterData data)
    {
        _movement.Setup(data);
        _health.Setup(data);

        _ragdoll.Rebuild();
    }

    protected virtual void HandleDeath()
    {
        _ragdoll.Kill();

        OnDeath?.Invoke(this);

        enabled = false;
    }

    private void OnHealthOver() => HandleDeath();
}