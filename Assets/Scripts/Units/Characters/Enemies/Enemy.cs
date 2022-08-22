using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterAppearanceChanger))]

public class Enemy : Character
{
    [BoxGroup("Data"), SerializeField] private EnemyData _data;
    [BoxGroup("Detect Parameters"), SerializeField] private LayerMask _aimLayerMask;
    
    private CharacterAppearanceChanger _appearanceChanger;
    private PlayerDetector _playerDetector;

    private void Awake()
    {
        _playerDetector = GetComponent<PlayerDetector>();
        _appearanceChanger = GetComponent<CharacterAppearanceChanger>();
    }

    private void Start() => Setup();

    private void Setup()
    {
        base.Setup(_data);
        
        _appearanceChanger.SetRandomAppearance();
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
    }
}