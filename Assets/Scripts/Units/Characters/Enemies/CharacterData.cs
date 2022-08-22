using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    public float Speed = 0.5f;
    public float AggroDuration = 1f;
    public int MaxHealth = 1;
    public float ViewRadius = 50f;
}