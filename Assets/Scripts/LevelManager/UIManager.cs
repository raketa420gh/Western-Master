using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIPanel _startPanel;
    [SerializeField] private UIPanel _winPanel;
    [SerializeField] private UIPanel _hud;

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
}