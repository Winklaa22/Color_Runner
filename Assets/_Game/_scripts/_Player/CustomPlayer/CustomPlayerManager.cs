using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomPlayerManager : SceneSingleton<CustomPlayerManager>, ISaveable
{
    [SerializeField] private CustomPlayerSlot[] m_slots;
    [SerializeField] private CustomItemSO[] m_allItems;
    public CustomPlayerSlot[] Slots => m_slots;
    public delegate void OnItemChanged(CustomItemSO item);
    public OnItemChanged Entity_OnItemChanged;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddItem(CustomItemSO item)
    {
        m_slots.First(x => x.Type == item.Type).ItemSO = item;
        Entity_OnItemChanged?.Invoke(item);
    }

    public object CaptureState()
    {

        var names = new List<string>();

        foreach(var slot in m_slots)
        {
            names.Add(slot.ItemSO.name);
            Debug.Log(slot.ItemSO.name);
        }

        return new SaveData
        {

            Slots = new SerializedList(names)
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        if (saveData.Slots is null)
            return;

        foreach(var slot in saveData.Slots.List)
        {
            Debug.Log(slot);
        }

        for (int i = 0; i < m_slots.Length; i++)
        {
            m_slots[i].ItemSO = m_allItems.First(x => x.name == saveData.Slots.List[i]);
        }
    }

    [System.Serializable]
    private struct SaveData
    {
        public SerializedList Slots;
    }
}

[System.Serializable]
public class SerializedList
{
    public List<string> List;

    public SerializedList(List<string> list)
    {
        List = list;
    }
}
