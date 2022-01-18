using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] _pathElements;
    [SerializeField]
    private Transform[] _pathElementsForRotate;
    private int _movingToPoint = 0;
    private int _movingToPointForRotate = 0;


    private void OnDrawGizmos()
    {
        for(int i = 1; i < _pathElements.Length; i++) 
        {
            Gizmos.DrawLine(_pathElements[i - 1].position, _pathElements[i].position);
        }
        
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        while (true)
        {
            yield return _pathElements[_movingToPoint];
            _movingToPoint++;
        }
    }

    public IEnumerator<Transform> GetNextRotatePoint()
    {
        while (true)
        {
            yield return _pathElementsForRotate[_movingToPointForRotate];
            _movingToPointForRotate++;
        }
    }

}
