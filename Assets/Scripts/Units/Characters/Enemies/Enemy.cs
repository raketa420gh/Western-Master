using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(CharacterAppearanceChanger))]

public class Enemy : Character
{
    [BoxGroup("Data"), SerializeField] private EnemyData _data;

    [BoxGroup("Detect Parameters"), SerializeField]
    private LayerMask _aimLayerMask;

    private CharacterAppearanceChanger _appearanceChanger;
    private PlayerDetector _playerDetector;

    protected override void Awake()
    {
        base.Awake();
        
        _appearanceChanger = GetComponent<CharacterAppearanceChanger>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    private void Start() => Setup(_data);

    public void Death() => HandleDeath();

    protected override void Setup(CharacterData data)
    {
        base.Setup(data);

        _appearanceChanger.SetRandomAppearance();
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
    }
}