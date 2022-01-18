using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private const string LocalData = "LocalData";
    public Data AllData => _allData;
    private Data _allData;

    public void LoadData()
    {
        _allData = new Data();
        if (PlayerPrefs.HasKey(LocalData))
        {
            string saveJson = PlayerPrefs.GetString(LocalData);
            _allData = JsonUtility.FromJson<Data>(saveJson);
        }
        else
        {
            SetDefaultValues();
        }
    }
    public void SetDefaultValues()
    {
        _allData.currentLevel = 0;
        _allData.coinsCount = 0;
        SaveDataLocal();
    }
    public void SaveDataLocal()
    {
        string saveJson = JsonUtility.ToJson(_allData);
        PlayerPrefs.SetString(LocalData, saveJson);
        PlayerPrefs.Save();
    }
    public void SaveLevel()
    {
        _allData.currentLevel++;
        SaveDataLocal();
    }
    public void SaveCoins(int value)
    {
        _allData.coinsCount = value;
        SaveDataLocal();
    }
}
[System.Serializable]
public class Data
{
    public int currentLevel;
    public int coinsCount;
}