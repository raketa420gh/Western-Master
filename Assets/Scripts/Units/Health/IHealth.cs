public interface IHealth
{
    void ChangeHealth(int amount);
    void ToggleImmortal(bool isActive);
    
    bool IsImmortal { get; }
}