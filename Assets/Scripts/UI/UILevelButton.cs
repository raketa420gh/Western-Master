using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class UILevelButton : MonoBehaviour
{
    private Button _button;
    private bool _isUnlocked;
    private bool _isCompleted;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _isUnlocked = false;
        _isCompleted = false;
    }

    public void ToggleUnlock(bool isActive)
    {
        _isUnlocked = isActive;
        _button.interactable = isActive;
    }

    public void ToggleComplete(bool isActive) => _isCompleted = isActive;
}