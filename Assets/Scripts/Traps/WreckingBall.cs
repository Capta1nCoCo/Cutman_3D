using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float limit = 75f;
    [SerializeField] private bool randomStart;
    [SerializeField] private bool fromLeftToRight;

    private float _random = 0;

    private void Awake()
    {
        if (randomStart)
        {
            _random = Random.Range(0f, 1f);
        }
    }

    private void Update()
    {
        float angle = limit * Mathf.Sin(Time.time + _random * speed);
        transform.localRotation = fromLeftToRight ? Quaternion.Euler(0, 0, -angle) : Quaternion.Euler(0, 0, angle);
    }
}
