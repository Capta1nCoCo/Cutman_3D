using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("[Controls]")]
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Joystick dynamicJoistick;

    [Header("[Pushback]")]
    [SerializeField] private float durationInSeconds = 0.2f;
    [SerializeField] private float trapForce = 100f;    
    [SerializeField] private float circularBladeForce = 30f;
    [SerializeField] private float bossForce = 15f;

    private const string Speed = "Speed";
    private const string WinAnimation = "WinAnimation";    

    private bool _isLife = true;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private Animator _animator;    

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();        
        GameEvent.LevelCompleted += OnLevelCompleted;
    }
    
    private void FixedUpdate()
    {
        if (!_isLife) return;
        MoveCharacter();
        _animator.SetFloat(Speed, Vector3.Magnitude(_rigidbody.velocity));        
    }

    private void OnDestroy()
    {
        GameEvent.LevelCompleted -= OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {        
        StopMovement();        
        _transform.rotation = Quaternion.Euler(0, 180f, 0);
        _animator.SetTrigger(WinAnimation);
        var effects = EffectManager.Instance;
        var winVFXRight = effects.GetWinVFX();
        var winVFXLeft = effects.GetWinVFX();
        winVFXRight.transform.position = new Vector3(_transform.position.x + 2f, _transform.position.y + 1.5f, _transform.position.z);
        winVFXLeft.transform.position = new Vector3(_transform.position.x + -2f, _transform.position.y + 1.5f, _transform.position.z);
    }

    private void MoveCharacter()
    {
        if (!dynamicJoistick.IsTouch)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        _rigidbody.velocity = new Vector3(dynamicJoistick.Horizontal * speed, _rigidbody.velocity.y, dynamicJoistick.Vertical * speed);
        if (Mathf.Abs(_rigidbody.velocity.x) < 0.1f && Mathf.Abs(_rigidbody.velocity.z) < 0.1f) return;
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, Quaternion.LookRotation(_rigidbody.velocity), rotationSpeed);
    }

    private void StopMovement()
    {
        _isLife = false;
        _rigidbody.velocity = Vector3.zero;
        dynamicJoistick.gameObject.SetActive(false);
    }

    private void StartMovement()
    {
        _isLife = true;
        _rigidbody.velocity = Vector3.zero;
        dynamicJoistick.gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Constants.Layers.Trap)
            ApplyPushback(collision, trapForce);
        else if (collision.gameObject.layer == Constants.Layers.CircularBlade)
            ApplyPushback(collision, circularBladeForce);
        else if (collision.gameObject.layer == Constants.Layers.Boss)
            ApplyPushback(collision, bossForce);
    }

    private void ApplyPushback(Collision collision, float force)
    {
        StopMovement();
        var direction = transform.position - collision.transform.position;
        direction.Normalize();
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(EnableMovementWithDelay());
    }

    private IEnumerator EnableMovementWithDelay()
    {
        yield return new WaitForSeconds(durationInSeconds);
        StartMovement();
    }
}
