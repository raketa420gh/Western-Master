using UnityEngine;

public class UILevelButton : MonoBehaviour
{
    private bool _isUnlocked;
    private bool _isCompleted;

    private void Start()
    {
        _isUnlocked = false;
        _isCompleted = false;
    }

    public void ToggleUnlock(bool isActive) => _isUnlocked = isActive;

    public void ToggleComplete(bool isActive) => _isCompleted = isActive;
}