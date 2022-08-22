using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(HumanoidRagdoll))]

public class Character : MonoBehaviour
{
    public event Action<Character> OnCreated;
    public event Action<Character> OnDeath;
    
    [BoxGroup("Transform"), SerializeField] private Transform _centerTransform;
    [BoxGroup("Components"), SerializeField]private CharacterMovement _movement;
    [BoxGroup("Components"), SerializeField]private HumanoidRagdoll _ragdoll;
    
    public Transform CenterTransform => _centerTransform;

    protected void Setup(CharacterData data)
    {
        _movement.Setup(data);
    }

    protected virtual void HandleDeath()
    {
        _ragdoll.Kill();

        OnDeath?.Invoke(this);

        enabled = false;
    }
}