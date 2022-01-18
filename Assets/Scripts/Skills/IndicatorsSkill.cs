using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorsSkill : MonoBehaviour
{
    [SerializeField] private GameObject waveIndicator;

    private void Awake()
    {
        waveIndicator.SetActive(false);
        GameEvent.WaveSkill += OnWaveSkill;
    }

    private void OnDestroy()
    {
        GameEvent.WaveSkill -= OnWaveSkill;
    }

    private void OnWaveSkill(bool state)
    {
        waveIndicator.SetActive(state);
    }
}
