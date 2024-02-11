using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataManager : SceneSingleton<SaveDataManager>
{
    private string _savePath => $"{Application.persistentDataPath}/save.txt";

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void OnStart()
    {
        base.OnStart();
        Load();
    }

    public void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    public void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(_savePath))
            return new Dictionary<string, object>();

        using FileStream fs = File.Open(_savePath, FileMode.Open);
        var formatter = new BinaryFormatter();
        return (Dictionary<string, object>)formatter.Deserialize(fs);
    }

    private void SaveFile(object state)
    {
        using FileStream fs = File.Open(_savePath, FileMode.Create);
        var formatter = new BinaryFormatter();
        formatter.Serialize(fs, state);
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach(var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.ID] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.ID, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
