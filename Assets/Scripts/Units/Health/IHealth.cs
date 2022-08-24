using System;

public interface IHealth
{
    event Action<int> OnChanged;
    event Action<float> OnPercentChanged; 
    event Action OnOver;
    
    public int Current { get; }
    bool IsImmortal { get; }

    void Setup(CharacterData data);
    void ChangeHealth(int amount);
    void ToggleImmortal(bool isActive);
}