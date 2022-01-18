using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSkill : MonoBehaviour
{
    [SerializeField] private Button waveButton;

    private void Awake()
    {
        waveButton.gameObject.SetActive(false);
        waveButton.onClick.AddListener(() => CastWave());
        GameEvent.WaveSkill += OnWaveSkill;
    }

    private void OnDestroy()
    {
        GameEvent.WaveSkill -= OnWaveSkill;
    }

    public void CastWave()
    {
        WeaponMovement.Instance.CastHorizontalWave();
        Wave.Instance.CreateHorizontalWave();
    }

    private void OnWaveSkill(bool state)
    {
        waveButton.gameObject.SetActive(state);
    }    
}
