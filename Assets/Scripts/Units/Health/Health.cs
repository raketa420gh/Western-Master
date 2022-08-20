using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public event Action<int> OnChanged;
    public event Action<float> OnPercentChanged = delegate {  }; 
    public event Action OnOver;
    
    private int _max = 100;
    
    public int Current { get; private set; }
    public bool IsImmortal { get; private set; }

    public void Setup(int maxHealth)
    {
        _max = maxHealth;
        Restore();
    }

    public void ChangeHealth(int amount)
    {
        Current += amount;
        OnChanged?.Invoke(amount);

        var currentHealthPercent = (float) Current / (float) _max;
        OnPercentChanged?.Invoke(currentHealthPercent);

        if (Current <= 0)
            OnOver?.Invoke();
    }

    public void ToggleImmortal(bool isActive) => IsImmortal = isActive;

    private void Restore() => 
        Current = _max;
}