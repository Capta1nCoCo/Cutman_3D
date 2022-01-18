using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float slowCoefficient = 0.4f;
    [SerializeField] private float durationInSeconds = 2f;

    private const float NormalTimeScale = 1;

    private void Awake()
    {
        GameEvent.SlowMotionEffect += SlowMotionEffect;
    }

    private void OnDestroy()
    {
        GameEvent.SlowMotionEffect -= SlowMotionEffect;
    }

    private void SlowMotionEffect()
    {
        StartCoroutine(SlowMotionSequence());
    }

    private IEnumerator SlowMotionSequence()
    {
        Time.timeScale = slowCoefficient;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSecondsRealtime(durationInSeconds);
        Time.timeScale = NormalTimeScale;        
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
