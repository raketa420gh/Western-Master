using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy", fileName = "EnemyData", order = 51)]

public class EnemyData : ScriptableObject
{
    public float Speed = 0.5f;
    public float AggroDuration = 1f;
    public int MaxHealth = 1;
    public float ViewRadius = 50f;
}