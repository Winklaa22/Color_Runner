using System;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Coins { get; set; }

    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
