using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSkill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Layers.Player)
        {
            GameEvent.WaveSkill(true);
            gameObject.SetActive(false);
        }
    }
}
