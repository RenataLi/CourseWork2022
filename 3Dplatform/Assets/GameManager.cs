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
    
    
    private void Awake()
    {
        if (_startPosition == null)
        {
            Debug.LogError("Start position is empty!");
        }

        _player = FindObjectOfType<Player>();
        if (_player == null)
        {
            _player = Instantiate(_playerPrefab,
                new Vector3(_startPosition.position.x + 2, _startPosition.position.y + 1, 0), Quaternion.identity).GetComponent<Player>();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        var currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentIndex);

        var allScenesLength = SceneManager.GetAllScenes().Length;

        if (currentIndex >= allScenesLength)
        {
            Debug.Log("Menu");
        }
        else
        {
            SceneManager.LoadScene(++currentIndex);
        }
    }
}
