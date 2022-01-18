using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    [SerializeField] private LevelsStorage levelsStorage;
    private int _coin;
    private static int _amountOfEnemies = 0;
    private int _currentLevelNumber;
    public int AmountOfEnemies => _amountOfEnemies;
    public int Coin => _coin;
    public int CurrentLevelNumber =>_currentLevelNumber;
    private SaveData _saveData;

    private void Awake()
    {
        Instance = this;
        _saveData = GetComponent<SaveData>();
        _saveData.LoadData();
        _coin = _saveData.AllData.coinsCount;
        _currentLevelNumber = _saveData.AllData.currentLevel;
        GameObject level = Instantiate(levelsStorage.GetLevel(_currentLevelNumber));
        level.transform.position = Vector3.zero;

        GameEvent.LevelCompleted += LevelCompleted;
        GameEvent.AddEnemy += AddEnemy;
        GameEvent.ReduceEnemy += ReduceEnemy;
    }

    private void OnDestroy()
    {
        GameEvent.LevelCompleted -= LevelCompleted;
        GameEvent.AddEnemy -= AddEnemy;
        GameEvent.ReduceEnemy -= ReduceEnemy;
    }

    
    public void AddCoins(int amountCoins)
    {
        _coin += amountCoins;
        _saveData.SaveCoins(_coin);
    }

    private void LevelCompleted()
    {
        _saveData.SaveLevel();
    }

    private void AddEnemy()
    {
        _amountOfEnemies++;
        Debug.Log(_amountOfEnemies);
    }

    private void ReduceEnemy()
    {
        _amountOfEnemies--;
        if(_amountOfEnemies < 1)
        {
            Invoke("AllEnemyDie", 1);
            
        }
    }

    private void AllEnemyDie()
    {
        GameEvent.LevelCompleted?.Invoke();
    }

    
}
