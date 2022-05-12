using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _scoresText;
    [SerializeField] private GameObject _completeLevelPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameManager _gm;
    private SettingsUi _settingsUi;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _settingsUi = FindObjectOfType<SettingsUi>();
    }

    public void UpdateHealth(string newHealth)
    {
        _healthText.text = newHealth;
    }
    
    public void UpdateScores(string newScores)
    {
        _scoresText.text = newScores;
    }

    public void ShowPause(bool isPause)
    {
        _pausePanel.SetActive(isPause);
    }
    
    public void ContinueClick()
    {
        _gm.SetPause();
        ShowLevelComplete(false);
    } 
    
    public void WinContinueClick()
    {
        _gm.WinLevel();
    }
    
    public void ExitClick()
    {
        _gm.BackMenu();
    }
    
    public void SettingsClick()
    {
        _settingsUi.ShowSettings(true);
    }

    public void ShowLevelComplete(bool isShow)
    {
        _completeLevelPanel.SetActive(isShow);
    }
}
