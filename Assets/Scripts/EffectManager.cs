using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    [SerializeField] private GameObject wavePrefab;
    [SerializeField] private GameObject winVFXPrefab;
    [SerializeField] private int poolSize = 2;

    private Queue<GameObject> wavePool = new Queue<GameObject>();
    private Queue<GameObject> winVFXPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        SpawnWaves();
        SpawnWinVFX();
    }

    private void SpawnWaves()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var wave = Instantiate(wavePrefab);
            wavePool.Enqueue(wave);
            wave.SetActive(false);
        }
    }

    private void SpawnWinVFX()
    {
        for (var i = 0; i < poolSize; i++)
        {
            var winVFX = Instantiate(winVFXPrefab);
            winVFXPool.Enqueue(winVFX);
            winVFX.SetActive(false);
        }
    }

    public GameObject GetWave()
    {
        GameObject wave = wavePool.Dequeue();
        wave.SetActive(true);
        wavePool.Enqueue(wave);
        return wave;
    }

    public GameObject GetWinVFX()
    {
        GameObject winVFX = winVFXPool.Dequeue();
        winVFX.SetActive(true);
        winVFXPool.Enqueue(winVFX);
        return winVFX;
    }

}
