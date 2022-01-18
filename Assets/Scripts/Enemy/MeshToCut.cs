using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshToCut : MonoBehaviour
{
    [SerializeField] private GameObject myEnemy;

    private void OnDisable()
    {
        if (myEnemy != null)
            myEnemy.SetActive(false);        
        GameEvent.FillSliderScale?.Invoke();
    }
}
