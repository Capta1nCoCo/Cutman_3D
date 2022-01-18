using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float delayInSeconds = 1f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(DisableObjectWithDelay());        
    }

    private void Update()
    {        
        _rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    private IEnumerator DisableObjectWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameObject.SetActive(false);
    }
}
