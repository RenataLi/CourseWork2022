using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector3 _direction;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        transform.Translate(_direction * (_moveSpeed) * Time.deltaTime);
    }

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null)
        {
            Destroy(gameObject);
            return;
        }
        
        damageable.ApplyDamage(1);
        
        Destroy(gameObject);
    }
}
