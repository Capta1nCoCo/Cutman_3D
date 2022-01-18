using System;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static Action SlowMotionEffect;
    public static Action LevelCompleted;
    public static Action AddEnemy;
    public static Action ReduceEnemy;
    public static Action FillSliderScale;
    public static Action WatchedAds;

    //[Skill Section]:
    public static Action<bool> WaveSkill;
}
