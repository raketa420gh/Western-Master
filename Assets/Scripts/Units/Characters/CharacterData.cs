using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _aggroDuration = 1f;
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _viewRadius = 50f;

    public float MoveSpeed => _moveSpeed;
    public int MaxHealth => _maxHealth;
    public float AggroDuration => _aggroDuration;
}