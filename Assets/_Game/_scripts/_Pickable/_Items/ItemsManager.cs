
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : SceneSingleton<ItemsManager>
{
    [SerializeField] private ItemInfo[] m_items;

    public PickableItem DrawItem()
    {
        float totalProbability = 0f;
        foreach (var item in m_items)
        {
            totalProbability += item.PropabilityNumbe;
        }

        float randomValue = Random.Range(0f, totalProbability);

        foreach (var item in m_items)
        {
            if (randomValue <= item.PropabilityNumbe)
            {
                return item.Item;
            }
            randomValue -= item.PropabilityNumbe;
        }

        return m_items[0].Item;
    }
}
