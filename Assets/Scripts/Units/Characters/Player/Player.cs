using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(HumanoidRagdoll))]
[RequireComponent(typeof(EnemyDetector))]

public class Player : MonoBehaviour
{
    public event Action OnCreated;
    public event Action OnDeath;

    [BoxGroup("Data"), SerializeField] private PlayerData _data;
    [BoxGroup("Transform"), SerializeField] private Transform _centerTransform;
    private CharacterMovement _movement;
    private HumanoidRagdoll _ragdoll;
    private EnemyDetector _detector;
    
    public EnemyDetector Detector => _detector;
    public Transform CenterTransform => _centerTransform;

    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _ragdoll = GetComponent<HumanoidRagdoll>();
        _detector = GetComponent<EnemyDetector>();
    }

    private void Start() => Setup();

    private void Setup()
    {
        _movement.Setup(_data.Speed);
    }

    public void HandleDeath()
    {
        _ragdoll.Kill();
        
        OnDeath?.Invoke();
        
        enabled = false;
    }
}