using UnityEngine;

[CreateAssetMenu(fileName = "Levels Storage", menuName = "Configs/Levels Storage", order = 1)]
public class LevelsStorage : ScriptableObject
{
    [SerializeField] private GameObject[] _levels;
    public int Count => _levels.Length;
    public GameObject GetLevel(int index) => _levels[index % Count];
}

