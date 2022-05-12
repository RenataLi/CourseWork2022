using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private int _distanceLength;
    [SerializeField] private int _depthLength;

    [SerializeField] private GameObject[] _trees;
    [SerializeField] private Transform _startPosition;

    [SerializeField] private Vector3 _currentPosition;

    private void Start()
    {
        _currentPosition = _startPosition.position;
        Generate();
    }

    private void Generate()
    {
        var maxDistance = Mathf.FloorToInt(_startPosition.position.x) + _distanceLength;
        var maxDepth = Mathf.FloorToInt(_startPosition.position.z) - _depthLength;
        
        while (Mathf.FloorToInt(_currentPosition.z) > maxDepth)
        {
            while (Mathf.FloorToInt(_currentPosition.x) < maxDistance)
            {
                var newPosition = new Vector3(_currentPosition.x + Random.Range(3, 8),
                    _currentPosition.y,
                    _currentPosition.z);
                var gameObj = Instantiate(GetRandomTree(), newPosition, Quaternion.Euler(new Vector3(
                    Random.Range(-8, 8), Random.Range(0, 360), Random.Range(-8, 8))));
                _currentPosition = gameObj.transform.position;
                gameObj.transform.localScale =
                    new Vector3(Random.Range(2f, 3.5f), Random.Range(2f, 4.5f), Random.Range(2f, 4.5f));
            }

            _currentPosition = new Vector3(_startPosition.position.x, _currentPosition.y,
                _currentPosition.z - Random.Range(4.5f, 7f));
        }
    }

    private GameObject GetRandomTree()
    {
        return _trees[Random.Range(0, _trees.Length)];
    }
}