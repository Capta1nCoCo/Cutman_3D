using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public static Wave Instance;

    [SerializeField] private Transform player;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateHorizontalWave()
    {
        GameEvent.WaveSkill(false);
        var waveInstance = EffectManager.Instance.GetWave();
        waveInstance.transform.position = new Vector3(player.position.x, player.position.y + 1f, player.position.z + 1f);
        waveInstance.transform.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y, Random.Range(-45f, 45f));        
    }
    
}
