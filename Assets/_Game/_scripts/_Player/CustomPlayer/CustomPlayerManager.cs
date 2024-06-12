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
    [SerializeField] private List<CustomItemSO> m_playersItems;
    public GenderType Gender => m_gender;
    public CustomPlayerSlot[] Slots => Gender == GenderType.MALE ? m_maleSlots: m_femaleSlots;

    public delegate void OnItemChanged(CustomItemSO item, GenderType gender);
    public OnItemChanged Entity_OnItemChanged;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void OnStart()
    {
        base.OnStart();
        GetAllDefaultItems();
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

    private void GetAllDefaultItems()
    {
        foreach(var item in m_allItems)
        {
            if(item.IsDefault && !m_playersItems.Contains(item))
            {
                m_playersItems.Add(item);
            }
        }
    }

    public bool IsItemUnlocked(string name) => m_playersItems.Any(x => x.name == name);

    public void AddItem(CustomItemSO item)
    {
        Slots.First(x => x.Type == item.Type).ItemSO = item;
        Entity_OnItemChanged?.Invoke(item, m_gender);
    }

    public object CaptureState()
    {

        var maleNames = m_maleSlots.Select(slot => slot.ItemSO.name).ToList();
        var femaleNames = m_femaleSlots.Select(slot => slot.ItemSO.name).ToList();
        var playersItemsNames = m_playersItems.Count > 0 ? m_playersItems.Select(item => item.name).ToList() : new List<string>();

        return new SaveData
        {

            MaleSlots = new SerializedList(maleNames),
            FemaleSlots = new SerializedList(femaleNames),
            PlayersItems = new SerializedList(playersItemsNames),
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

        if (saveData.PlayersItems != null)
        {
            foreach(var item in saveData.PlayersItems.List)
            {
                if (m_playersItems.Any(x => x.name == item) || m_allItems.All(x => x.name != item))
                    continue;

                m_playersItems.Add(m_allItems.First(x => x.name == item));
            }
        }


    }

    [System.Serializable]
    private struct SaveData
    {
        public SerializedList MaleSlots;
        public SerializedList FemaleSlots;
        public SerializedList PlayersItems;
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
