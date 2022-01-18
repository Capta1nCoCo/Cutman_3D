using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private float _maxDistance = 0.1f;
    [SerializeField]
    private float _rotateDistance = 1;
    [SerializeField]
    private float _rotateTime = 3;
    [SerializeField]
    private MovementPath _movementPath;
    private bool _isRight;
    private int _degreeOfRotation = 0;
    private IEnumerator<Transform> _pointInPath;
    private IEnumerator<Transform> _pointForRotate;


    private void Start()
    {
        _pointInPath = _movementPath.GetNextPathPoint();
        _pointInPath.MoveNext();
        transform.position = _pointInPath.Current.position;

        _pointForRotate = _movementPath.GetNextRotatePoint();
        _pointForRotate.MoveNext();                
    }

    private void Update()
    {
        MovePlayer();
        CheckPathPoint();
        CheckRotatePoint();
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _pointInPath.Current.position, Time.deltaTime * _speed);
    }

    private void CheckPathPoint()
    {
        float maxDistanceSqure = (transform.position - _pointInPath.Current.position).sqrMagnitude;
        if (maxDistanceSqure < _maxDistance * _maxDistance)
        {
            _pointInPath.MoveNext();
        }
    }

    private void CheckRotatePoint()
    {
        float rotateDistanceSqure = (transform.position - _pointForRotate.Current.position).sqrMagnitude;

        if (rotateDistanceSqure < _rotateDistance * _rotateDistance)
        {
            _degreeOfRotation = _isRight ? 90 : 0;
            LeanTween.rotateY(gameObject, _degreeOfRotation, _rotateTime);
            _isRight = _isRight ? false : true;
            _pointForRotate.MoveNext();
        }
    }
}
