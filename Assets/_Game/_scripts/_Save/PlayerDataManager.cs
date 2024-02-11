using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataManager : SceneSingleton<PlayerDataManager>
{
    private int _coins;
    public int Coins => _coins;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(Instance.gameObject);
    }

    protected override void OnStart()
    {
        base.OnStart();
        LoadJsonData();
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
    }

    public void SaveJsonData()
    {
        var saveData = new SaveData();
        SetPopulateSaveData(ref saveData);
        Debug.Log("Save json: " + saveData.SaveToJson());
        File.WriteAllText(Application.persistentDataPath + "/savedData.json", saveData.SaveToJson());
    }

    private void LoadJsonData()
    {
        var saveData = new SaveData();
        var json = File.ReadAllText(Application.persistentDataPath + "/savedData.json");
        saveData.LoadFromJson(json);
        SetLoadSaveData(saveData);
    }

    public void SetPopulateSaveData(ref SaveData saveData)
    {
        saveData.Coins = _coins;
    }

    public void SetLoadSaveData(SaveData saveData)
    {
        _coins = saveData.Coins;
    }
}
