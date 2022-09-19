using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private UIPanel _mainMenuPanel;
    [SerializeField] private UIPanel _selectLevelPanel;
    [SerializeField] private UIPanel _settingsPanel;
    [SerializeField] private List<UILevelButton> _allLevelButtons = new List<UILevelButton>();

    private List<UIPanel> _allPanels = new List<UIPanel>();
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader) =>
        _sceneLoader = sceneLoader;

    private void Start()
    {
        for (var i = 0; i < _allPanels.Count; i++)
        {
            if (_allPanels[i] != null)
                _allPanels.Add(_allPanels[i]);
        }
        
        for (var i = 0; i < _allLevelButtons.Count; i++)
        {
            if (_allLevelButtons[i] != null)
                _allLevelButtons.Add(_allLevelButtons[i]);
        }

        ShowMainMenuPanel();
        
        UpdateLevelsView();
    }

    private void UpdateLevelsView()
    {
        foreach (var level in _allLevelButtons)
            level.ToggleUnlock(false);

        _allLevelButtons[0].ToggleUnlock(true);
    }

    public void ShowMainMenuPanel()
    {
        foreach (var panel in _allPanels)
            panel.Hide();
        
        _mainMenuPanel.Show();
    }

    public void ShowSelectLevelPanel()
    {
        foreach (var panel in _allPanels)
            panel.Hide();

        _selectLevelPanel.Show();
    }

    public void LoadLevel(int levelNumber) => _sceneLoader.LoadScene(LevelNames.Level + levelNumber);
}