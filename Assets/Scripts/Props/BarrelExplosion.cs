using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] private GameObject[] waves;
    
    private void OnDisable()
    {
        foreach (GameObject wave in waves)
        {
            wave.SetActive(true);
        }
        GameEvent.SlowMotionEffect?.Invoke();
    }    
}
