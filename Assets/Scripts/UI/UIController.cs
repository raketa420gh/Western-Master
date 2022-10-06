using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIController : MonoBehaviour
{
    [BoxGroup("Panels"), SerializeField] private UIPanel _startPanel;
    [BoxGroup("Panels"), SerializeField] private UIPanel _winPanel;
    [BoxGroup("Panels"), SerializeField] private UIPanel _hud;
    [BoxGroup("Weapon"), SerializeField] private TMP_Text _bulletsCountText;
    [BoxGroup("LevelProgress"), SerializeField] private Image _levelProgressImage;
    
    private Player _player;

    [Inject]
    public void Construct(Player player) => _player = player;

    private void OnEnable()
    {
        _player.Weapon.OnBulletCountChanged += OnPlayerWeaponBulletCountChanged;
    }

    private void Update()
    {
        _levelProgressImage.fillAmount = (float) _player.SplineFollower.result.percent;
    }

    private void OnDisable()
    {
        _player.Weapon.OnBulletCountChanged -= OnPlayerWeaponBulletCountChanged;
    }

    public void ToggleStartPanel(bool isActive)
    {
        if (isActive)
            _startPanel.Show();
        else
            _startPanel.Hide();
    }

    public void ToggleWinPanel(bool isActive)
    {
        if (isActive)
            _winPanel.Show();
        else
            _winPanel.Hide();
    }

    public void ToggleHUD(bool isActive)
    {
        if (isActive)
            _hud.Show();
        else
            _hud.Hide();
    }

    private void OnPlayerWeaponBulletCountChanged(int current)
    {
        _bulletsCountText.text = current.ToString();
    }
}