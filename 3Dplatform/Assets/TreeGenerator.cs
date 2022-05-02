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

    [SerializeField] private Transform _currentPosition;
    
    void Start()
    {
        _currentPosition = _startPosition;
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Generate()
    {
        var maxDistance = Mathf.Abs(Mathf.FloorToInt(_startPosition.position.x)) + _distanceLength;

        while (Mathf.FloorToInt(Mathf.Abs(_currentPosition.position.x)) < maxDistance)
        {
            var newPosition = new Vector3(_currentPosition.position.x + 3, _currentPosition.position.y,
                _currentPosition.position.z);
            _currentPosition = Instantiate(GetRandomTree(), newPosition, Quaternion.identity).transform;
        }
    }

    private GameObject GetRandomTree()
    {
        return _trees[Random.Range(0, _trees.Length)];
    }
}
