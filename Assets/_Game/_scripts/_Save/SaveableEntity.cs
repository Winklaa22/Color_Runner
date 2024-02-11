using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string m_id = string.Empty;

    public string ID => m_id;

    [ContextMenu("Generate Id")]
    private void GenerateID() => m_id = Guid.NewGuid().ToString();

    public object CaptureState()
    {
        var state = new Dictionary<string, object>();

        foreach(var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }

        return state;
    }

    public void RestoreState(object state)
    {
        var stateDictionary = state as Dictionary<string, object>;

        foreach(var saveable in GetComponents<ISaveable>())
        {
            var typeName = saveable.GetType().ToString();

            if(stateDictionary.TryGetValue(typeName, out var value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
