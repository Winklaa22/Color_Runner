using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfo
{
    [SerializeField] private float m_propabilityNumber;
    public float PropabilityNumbe => m_propabilityNumber;

    [SerializeField] private PickableItem m_item;
    public PickableItem Item => m_item;
}
