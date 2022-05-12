using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelsPanel;
    [SerializeField] private GameObject _tutorialPanel;

    [SerializeField] private Button[] _levelsButton;

    private SettingsUi _settingsUi;
    
    private void Start()
    {
        _settingsUi = FindObjectOfType<SettingsUi>();
        _levelsPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
        
        for (int i = 0; i < _levelsButton.Length; i++)
        {
            _levelsButton[i].interactable = false;
        }
        
        SettingsReader.Settings.InitSettings();

        Time.timeScale = 1;
    }

    public void ExitClick()
    {
        Application.Quit();
    }
    
    public void LevelsClick()
    {
        ActivateLevels();
        _levelsPanel.SetActive(true);
    }
    
    public void TutorialClick()
    {
        _tutorialPanel.SetActive(true);
    }
    
    public void SettingsClick()
    {
        _settingsUi.ShowSettings(true);
    }
    
    public void BackToMenu()
    {
        _levelsPanel.SetActive(false);
        _tutorialPanel.SetActive(false);
    }

    public void ActivateLevels()
    {
        var levels = SettingsReader.Settings.setting.CompleteLevels + 1;

        for (int i = 0; i < levels; i++)
        {
            _levelsButton[i].interactable = true;
        }
    }
    
}
