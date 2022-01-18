using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public static WeaponMovement Instance;

    private const string HorizontalWave = "HorizontalWave";

    private Animator _animator;
    
    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
        GameEvent.LevelCompleted += OnLevelCompleted;
    }    
    
    private void OnDestroy()
    {
        GameEvent.LevelCompleted -= OnLevelCompleted;
    }

    public void CastHorizontalWave()
    {
        _animator.SetTrigger(HorizontalWave);
    }

    private void OnLevelCompleted()
    {
        gameObject.SetActive(false);        
    }
}
