using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
	[SerializeField] private float distance = 5f;
	[SerializeField] private bool horizontal = true;
	[SerializeField] private float speed = 3f;
	[SerializeField] private float startOffset = 0f; 

	private bool isForward = true;
	private Vector3 startPos;

	void Awake()
	{
		startPos = transform.position;
		if (horizontal)
			transform.position += Vector3.right * startOffset;
		else
			transform.position += Vector3.forward * startOffset;
	}

	void Update()
	{
		if (horizontal)
		{
			if (isForward)
			{
				if (transform.position.x < startPos.x + distance)
				{
					transform.position += Vector3.right * Time.deltaTime * speed;
				}
				else
					isForward = false;
			}
			else
			{
				if (transform.position.x > startPos.x)
				{
					transform.position -= Vector3.right * Time.deltaTime * speed;
				}
				else
					isForward = true;
			}
		}
		else
		{
			if (isForward)
			{
				if (transform.position.z < startPos.z + distance)
				{
					transform.position += Vector3.forward * Time.deltaTime * speed;
				}
				else
					isForward = false;
			}
			else
			{
				if (transform.position.z > startPos.z)
				{
					transform.position -= Vector3.forward * Time.deltaTime * speed;
				}
				else
					isForward = true;
			}
		}
	}
}
