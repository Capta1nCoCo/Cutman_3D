using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Shooter : MonoBehaviour
{
    [SerializeField] 
    private Transform placeForBullet;
    [FormerlySerializedAs("_bullet")] [SerializeField] 
    private GameObject bullet;
    [SerializeField]
    private float rotationSpeed = 6;
    [SerializeField] 
    private float agrRadius = 10;
    [SerializeField]
    private GameObject meshToCut;
    private List<GameObject> _bullets;

    private Animator _animator;
    private Transform _transform;
    private Transform _playerTransform;
    
    private bool _isShooting;
    private const string Shooting = "Shooting";

    private void Awake()
    {
        _playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        GameEvent.AddEnemy?.Invoke();
        
        _bullets = new List<GameObject>();
        for (int i = 0; i < 25; i++)
        {
            _bullets.Add(Instantiate(bullet));
        }

        foreach (var bullet in _bullets)
        {
            bullet.SetActive(false);
        }
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
        CheckShootingDistance();
        if (_isShooting)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.Normalize(_playerTransform.position - transform.position)), rotationSpeed);
        }
    }
    
    private void CheckShootingDistance()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) < agrRadius)
        {
            _isShooting = true;
            _animator.SetBool(Shooting, true);
        }
        else if (Vector3.Distance(transform.position, _playerTransform.position) > agrRadius)
        {
            _isShooting = false;
            _animator.SetBool(Shooting, false);  
        }
        
    }

    private void Shoot()
    {
        foreach (var bullet in _bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                Debug.Log(!bullet.activeInHierarchy);
                bullet.transform.position = placeForBullet.position;
                bullet.transform.eulerAngles = _transform.eulerAngles;
                bullet.SetActive(true);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Layers.Cutter)
        {            
            meshToCut.transform.position = new Vector3(transform.position.x, transform.position.y + 1.11f, transform.position.z);
            meshToCut.transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, 0);
            meshToCut.SetActive(true);
        }
    }
}
