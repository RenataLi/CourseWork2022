using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable, IHeal, IScorable
{
    [SerializeField] private int _health;   
    [SerializeField] private int _scores;   
    [SerializeField] private int _maxHealth;   
    [SerializeField] private float _jumpForce;    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _canJump;
    [SerializeField] private LayerMask _layerGround;
    private GameManager _gm;
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameUi _ui;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _shootTransform;
    [SerializeField] private Transform _jumpTransform;
    
    
    private void Start()
    {
        _health = _maxHealth;
        _rigidbody = GetComponent<Rigidbody>();
        _ui = FindObjectOfType<GameUi>();
        _gm = FindObjectOfType<GameManager>();

        InitUserInterface();
    }

    private void Update()
    {
        if (_gm.isPaused)
        {
            return;
        }
        
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void InitUserInterface()
    {
        _ui.UpdateHealth(_health.ToString());
        _ui.UpdateScores(_scores.ToString());
    }
    
    private void ChangeHealth(int value)
    {
        _health += value;
    }
    
    private bool IsAlive()
    {
        return _health > 0;
    }
    
    public void ApplyDamage(int damage)
    {
        ChangeHealth(-damage);
        
        if (!IsAlive())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        _ui.UpdateHealth(_health.ToString());

    }
    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W) && CanJump())
        {
            Jump();
        }

        var movement = Input.GetAxisRaw("Horizontal");
        if (movement != 0)
        {
            Flip(movement);
        }
            
        _rigidbody.velocity = 
            new Vector3(movement * _moveSpeed, _rigidbody.velocity.y, _rigidbody.velocity.z) ;
    }

    private void Shoot()
    {
        var go = Instantiate(_bullet, _shootTransform.position, Quaternion.identity);
        var direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        go.GetComponent<Bullet>().Initialize(direction);
    }
    
    private void Flip(float direction)
    {
        var newValue = direction > 0 ? 1 : -1;
        transform.localScale = new Vector3(newValue, transform.localScale.y, transform.localScale.z);
    }

    public bool OnHeal(int healValue)
    {
        if (_health >= _maxHealth)
        {
            return false;
        }
        
        ChangeHealth(healValue);
        _ui.UpdateHealth(_health.ToString());
        return true;
    }

    public void AddSores(int value)
    {
        _scores += value;
        _ui.UpdateScores(_scores.ToString());
    }

    private bool CanJump()
    {
        return Physics.CheckSphere(_jumpTransform.position, .1f, _layerGround);
    }
}
