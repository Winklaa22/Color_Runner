using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataManager : SceneSingleton<PlayerDataManager>
{
    private int _coins;

    public void SaveJsonData()
    {
        var saveData = new SaveData();
        SetPopulateSaveData(saveData);

        File.WriteAllText(Application.dataPath + "/savedData.json", saveData.SaveToJson());
    }

    private void LoadJsonData()
    {
        var saveData = new SaveData();
        var json = File.ReadAllText(Application.dataPath + "/savedData.json");
        saveData.LoadFromJson(json);
        SetLoadSaveData(saveData);
    }

    public void SetPopulateSaveData(SaveData saveData)
    {
        saveData.Coins = _coins;
    }

    public void SetLoadSaveData(SaveData saveData)
    {
        _coins = saveData.Coins;
    }
}
