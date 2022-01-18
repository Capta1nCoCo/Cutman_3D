using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float moveSpeedCamera;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GetComponent<Transform>();
        cameraTransform.position = player.position + offset / 2;
    }

    private void LateUpdate()
    {
        cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, player.position + offset, moveSpeedCamera * Time.deltaTime);
    }


}