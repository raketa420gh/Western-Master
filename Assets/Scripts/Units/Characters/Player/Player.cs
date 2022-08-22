using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(EnemyDetector))]

public class Player : Character
{
    [BoxGroup("Data"), SerializeField] private PlayerData _data;

    private EnemyDetector _detector;

    private void Awake()
    {
        _detector = GetComponent<EnemyDetector>();
    }
    
    private void Start() => Setup();

    private void Setup()
    {
        base.Setup(_data);
    }
}