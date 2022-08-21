using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event Action OnCreated;
    public event Action OnDeath;
    
    [BoxGroup("Transform"), SerializeField] private Transform _centerTransform;
    
    private CharacterMovement _movement;
    private HumanoidRagdoll _ragdoll;
    
    public Transform CenterTransform => _centerTransform;

    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _ragdoll = GetComponent<HumanoidRagdoll>();
    }

    public void HandleDeath()
    {
        _ragdoll.Kill();

        OnDeath?.Invoke();

        enabled = false;
    }
}