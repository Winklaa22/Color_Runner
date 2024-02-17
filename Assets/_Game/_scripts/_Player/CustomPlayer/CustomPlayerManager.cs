using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomPlayerManager : SceneSingleton<CustomPlayerManager>
{
    [SerializeField] private CustomPlayerSlot[] m_slots;
    public CustomPlayerSlot[] Slots => m_slots;
    public delegate void OnItemChanged(CustomItemSO item);
    public OnItemChanged Entity_OnItemChanged;

    public void AddItem(CustomItemSO item)
    {
        m_slots.First(x => x.Type == item.Type).ItemSO = item;
        Entity_OnItemChanged?.Invoke(item);
    }
}
