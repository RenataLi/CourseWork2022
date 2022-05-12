using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Player _player;
    private GameUi _ui;
    public bool isPaused;
    
    private void Awake()
    {
        if (_startPosition == null)
        {
            Debug.LogError("Start position is empty!");
        }

        _ui = FindObjectOfType<GameUi>();
        _player = FindObjectOfType<Player>();
        if (_player == null)
        {
            _player = Instantiate(_playerPrefab,
                new Vector3(_startPosition.position.x + 2, _startPosition.position.y + 1, 0), Quaternion.identity).GetComponent<Player>();
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        Time.timeScale = 0;
        _ui.ShowLevelComplete(true);
    }

    public void SetPause()
    {
        isPaused = !isPaused;
        _ui.ShowPause(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }
    
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void WinLevel()
    {
        Time.timeScale = 1;
        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentIndex);

        var allScenesLength = SceneManager.sceneCountInBuildSettings;

        if (currentIndex + 1 >= allScenesLength)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SettingsReader.Settings.setting.CompleteLevels = currentIndex;
            SettingsReader.Settings.UpdateSettings();
            SceneManager.LoadScene(++currentIndex);
        }
    }
}
