using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCleaner : MonoBehaviour
{
    private float delayInSeconds = 5f;

    private void OnEnable()
    {
        StartCoroutine(DisableObjectWithDelay());
    }

    private IEnumerator DisableObjectWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        gameObject.SetActive(false);
    }
}
