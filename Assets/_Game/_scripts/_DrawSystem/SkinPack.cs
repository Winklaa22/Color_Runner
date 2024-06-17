using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinPack 
{
    [SerializeField] private string m_name = "New Pack";
    public string Name => m_name;

    [SerializeField] private CustomItemSO[] m_items;
    public CustomItemSO[] Items => m_items;
}
