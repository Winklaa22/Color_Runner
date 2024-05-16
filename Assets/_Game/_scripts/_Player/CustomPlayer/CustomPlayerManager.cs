using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomPlayerManager : SceneSingleton<CustomPlayerManager>, ISaveable
{
    [SerializeField] private CustomPlayerSlot[] m_maleSlots;
    [SerializeField] private CustomPlayerSlot[] m_femaleSlots;
    [SerializeField] private GenderType m_gender;
    [SerializeField] private CustomItemSO[] m_allItems;
    public GenderType Gender => m_gender;
    public CustomPlayerSlot[] Slots => Gender == GenderType.MALE ? m_maleSlots: m_femaleSlots;
    public delegate void OnItemChanged(CustomItemSO item, GenderType gender);
    public OnItemChanged Entity_OnItemChanged;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeGender(GenderType gender)
    {
        m_gender = gender;
        var slots = gender == GenderType.MALE ? m_maleSlots : m_femaleSlots;
        foreach(var slot in slots)
        {
            Entity_OnItemChanged?.Invoke(slot.ItemSO, m_gender);
        }

    }

    public void AddItem(CustomItemSO item)
    {
        Slots.First(x => x.Type == item.Type).ItemSO = item;
        Entity_OnItemChanged?.Invoke(item, m_gender);
    }

    public object CaptureState()
    {

        var maleNames = new List<string>();
        var femaleNames = new List<string>();
        foreach(var slot in m_maleSlots)
        {
            maleNames.Add(slot.ItemSO.name);
        }

        foreach(var slot in m_femaleSlots)
        {
            femaleNames.Add(slot.ItemSO.name);
        }

        return new SaveData
        {

            MaleSlots = new SerializedList(maleNames),
            FemaleSlots = new SerializedList(femaleNames),
            GenderIndex = (int) m_gender
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        m_gender = (GenderType)saveData.GenderIndex;

        if (saveData.MaleSlots != null)
        {
            for (int i = 0; i < m_maleSlots.Length; i++)
            {
                m_maleSlots[i].ItemSO = m_allItems.First(x => x.name == saveData.MaleSlots.List[i]);
            }
        }

        if (saveData.FemaleSlots != null)
        {
            for (int i = 0; i < m_femaleSlots.Length; i++)
            {
                m_femaleSlots[i].ItemSO = m_allItems.First(x => x.name == saveData.FemaleSlots.List[i]);
            }
        }
        
    }

    [System.Serializable]
    private struct SaveData
    {
        public SerializedList MaleSlots;
        public SerializedList FemaleSlots;
        public int GenderIndex;
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
