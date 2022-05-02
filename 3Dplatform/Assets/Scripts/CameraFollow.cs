using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void Start()
    {
        if (_playerTransform == null)
        {
            _playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
        }
    }

    private void LateUpdate()
    {
        if (_playerTransform == null)
        {
            return;
        }

        transform.position =
            new Vector3(_playerTransform.transform.position.x,
                _playerTransform.transform.position.y + 2, transform.position.z);
    }
}
