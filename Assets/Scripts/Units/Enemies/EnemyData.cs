using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy", fileName = "EnemyData", order = 51)]

public class EnemyData : ScriptableObject
{
    public EnemyType Type = EnemyType.Common;
    public float Speed = 0.5f;
    public float AggroDuration = 1f;
    public int MaxHealth = 1;
    public float ViewRadius = 50f;
    public float DistanceToWallDetect = 1f;
}