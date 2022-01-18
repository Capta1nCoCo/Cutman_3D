using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _direction = transform.forward;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _direction * speed;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Constants.Layers.Player)
        {
            Debug.Log("Player has been killed!");
            gameObject.SetActive(false);
        }
        if (collision.gameObject.layer == Constants.Layers.Barrier)
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.layer == Constants.Layers.Enemy)
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.layer == Constants.Layers.Trap)
        {
            gameObject.SetActive(false);
        }
    }
}
