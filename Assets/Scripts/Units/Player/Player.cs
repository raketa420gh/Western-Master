using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnDeath;

    //[BoxGroup("Data"), SerializeField] private PlayerData _data;
    [BoxGroup("Components"), SerializeField] private HumanoidRagdoll _ragdoll;
    //[BoxGroup("Components"), SerializeField] private PlayerController _controller;
    //[BoxGroup("Components"), SerializeField] private PlayerMovement _movement;
    [BoxGroup("Components"), SerializeField] private EnemyDetector _enemyDetector;
    [BoxGroup("Parameters"), SerializeField] private Transform _playerCenterTransform;
    
    private Collider _collider;
    
    public EnemyDetector EnemyDetector => _enemyDetector;
    public Transform PlayerCenterTransform => _playerCenterTransform;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    //private void Start() => Setup();

    private void OnTriggerEnter(Collider other)
    {
        var deathDealer = other.gameObject.GetComponentInChildren<DeathDealer>();

        if (deathDealer)
            HandleDeath();
    }

    private void Setup()
    {
        //_controller.Setup(_data);
        //_movement.Setup(_data);
    }

    private void HandleDeath()
    {
        _ragdoll.Kill();
        _ragdoll.SetDeathRagdollSettings();
        
        _collider.enabled = false;
        //_controller.gameObject.SetActive(false);
        
        OnDeath?.Invoke();
        
        enabled = false;
    }
}