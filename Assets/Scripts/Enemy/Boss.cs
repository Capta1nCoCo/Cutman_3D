using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int hitPoints = 6;
    [SerializeField] private Image foregroundImage;    

    [Header("Movement")]
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float rotationSpeed = 6;
    private Transform _playerTransform;
    [SerializeField]
    private float agrRadius;
    [SerializeField]
    private GameObject meshToCut;
    private Vector3 _direction;
    private Transform _transform;
    private Rigidbody _rigidbody;
    private Animator _animator;

    private bool _isRunning;
    private float _maxHitPoints;

    private const string Speed = "Speed";
    private const string Attack = "Attack";

    private void Awake()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _maxHitPoints = hitPoints;
    }

    private void Start()
    {
        GameEvent.AddEnemy?.Invoke();
    }

    private void OnDisable()
    {
        GameEvent.ReduceEnemy?.Invoke();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction * speed;
        if (_isRunning)
        {
            _animator.SetFloat(Speed, Vector3.Magnitude(_rigidbody.velocity));
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.LookRotation(Vector3.Normalize(_playerTransform.position - _transform.position)), rotationSpeed);
        }
        CheckRunningDistance();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Constants.Layers.Player)
        {
            AttackToPlayer();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Layers.Cutter)
        {
            if (hitPoints > 0)
            {
                ReceiveDamage(1);
            }
            else
            {
                meshToCut.transform.position = new Vector3(transform.position.x, transform.position.y + 1.11f, transform.position.z);
                meshToCut.transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, 0);
                meshToCut.SetActive(true);
            }            
        }
        else if (other.gameObject.layer == Constants.Layers.Skill)
        {
            ReceiveDamage(2);                        
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == Constants.Layers.Player)
        {
            _animator.SetBool(Attack, false);
        }

    }

    private void CheckRunningDistance()
    {
        if (Vector3.Distance(_transform.position, _playerTransform.position) < agrRadius)
        {
            _isRunning = true;
            _direction = Vector3.Normalize(_playerTransform.position - _transform.position);
        }
    }
    private void AttackToPlayer()
    {
        _isRunning = false;
        _direction = Vector3.zero;
        _rigidbody.velocity = _direction;
        _animator.SetBool(Attack, true);
    }

    private void ReceiveDamage(int points)
    {
        hitPoints -= points;
        foregroundImage.fillAmount -= points / _maxHitPoints;
    }
}
