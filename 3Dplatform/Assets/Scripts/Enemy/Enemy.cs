using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _reloadTime;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector2 _positionRange;
    [SerializeField] private bool _isFaceRight;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _viewDistance;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _model;
    [SerializeField] private Transform _shootTransform;
    [SerializeField] private Transform _eyeTransform;
    
    private float _currentTime;
    private EnemyUI _enemyUI;

    private Vector3 _direction 
    {
        get
        {
            return _isFaceRight ? Vector3.right : Vector3.left;
        }
        
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyUI = GetComponent<EnemyUI>();
        
        _enemyUI.UpdateHealth(_health.ToString());
        _startPosition = transform.position;
        SynchronizeDirection();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vision();
        Movement();
    }

    private void Movement()
    {
        var pos = transform.position;

        if (!_isFaceRight)
        {
            if (Vector3.Distance(pos, 
                new Vector3(_startPosition.x - _positionRange.x, _startPosition.y, _startPosition.z)) > 1f)
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
                new Vector3(_startPosition.x + _positionRange.y, _startPosition.y, _startPosition.z)) > 1f)
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
        _model.transform.localEulerAngles = new Vector3(_model.transform.localEulerAngles.x,
            _model.transform.localEulerAngles.y * -1, _model.transform.localEulerAngles.z);
        
        _shootTransform.transform.localEulerAngles = new Vector3(_shootTransform.transform.localEulerAngles.x,
            _shootTransform.transform.localEulerAngles.y * -1, _shootTransform.transform.localEulerAngles.z);
    }

    private void SynchronizeDirection()
    {
        _isFaceRight = _model.transform.rotation.y > 0; //_model.transform.localScale.x > 0;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        
        if (_health < 1)
        {
            Destroy(gameObject);
        }

        _enemyUI.UpdateHealth(_health.ToString());
    }

    private void Vision()
    {
        RaycastHit hit;
        var direction = _isFaceRight ? Vector3.right : Vector3.left;
        if (Physics.Raycast(_eyeTransform.position, _eyeTransform.TransformDirection(direction), out hit, _viewDistance))
        {
            if (hit.collider.GetComponent<Player>())
            {
                Debug.DrawRay(_eyeTransform.position, _eyeTransform.TransformDirection(direction) * hit.distance, Color.yellow);
                Shoot(); 
            }
        }
        else
        {
            Debug.DrawRay(_eyeTransform.position, _eyeTransform.TransformDirection(direction) * _viewDistance, Color.white);
        }
    }

    private void Shoot()
    {
        if (_currentTime < _reloadTime)
        {
            return;
        }

        _currentTime = 0;
        var go = Instantiate(_bullet, _shootTransform.position, Quaternion.identity);
        go.GetComponent<Bullet>().Initialize(_direction);
    }
}
