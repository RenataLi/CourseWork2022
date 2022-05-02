using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    [SerializeField] public int _impulsePower;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null)
        {
            return;
        }
        
        damageable.ApplyDamage(1);

        var rb = other.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * _impulsePower, ForceMode.Impulse);
    }
}
