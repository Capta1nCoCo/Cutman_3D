using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerTrap : MonoBehaviour
{
    [SerializeField] private float turningSpeed = 100f;
    [SerializeField] private bool clockwise;

    private Vector3 _spinDirection;

    private void Awake()
    {
        _spinDirection = clockwise ? -Vector3.down : Vector3.down;
    }

    private void Update()
    {
        transform.Rotate(_spinDirection * turningSpeed * Time.deltaTime);
    }
}
