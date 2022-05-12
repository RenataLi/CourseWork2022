using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private bool _isRotate;
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        if (_isRotate)
            transform.Rotate (Vector3.forward * _rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        
        player.ApplyDamage(1);
        var playerRb =  player.GetComponent<Rigidbody>();
        playerRb.AddForce(playerRb.velocity * -1 * 150);
    }
}
