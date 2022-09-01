using UnityEngine;

[CreateAssetMenu(menuName = "Data/Units/Enemy", fileName = "EnemyData", order = 51)]

public class EnemyData : CharacterData
{
    [SerializeField] private EnemyAggroType _aggroType = EnemyAggroType.Simply;

    public EnemyAggroType AggroType => _aggroType;
}