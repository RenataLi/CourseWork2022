using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private bool _isFaceRight;
    [SerializeField] private float _reloadTime;  
    [SerializeField] private Vector2 _positionRange;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        var pos = transform.position;

        if (!_isFaceRight)
        {
            if (Vector3.Distance(pos, 
                new Vector3(_startPosition.x - _positionRange.x, _startPosition.y, _startPosition.z)) > 0.2f)
            {
                _rigidbody.MovePosition(new Vector3(pos.x + (_moveSpeed * -_positionRange.x) *  Time.deltaTime, pos.y, pos.z));
            }
            else
            {
                Flip();
            }
        }

        if (_isFaceRight)
        {
            if (Vector3.Distance(pos, 
                new Vector3(_startPosition.x + _positionRange.y, _startPosition.y, _startPosition.z)) > 0.2f)
            {
                _rigidbody.MovePosition(new Vector3(pos.x + (_moveSpeed * _positionRange.y) *  Time.deltaTime, pos.y, pos.z)) ;
            }
            else
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        _isFaceRight = !_isFaceRight;
    }
}
