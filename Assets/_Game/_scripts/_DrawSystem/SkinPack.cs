using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newSkinPack", menuName="Skin Pack")]
public class SkinPack : ScriptableObject
{
    [SerializeField] private string m_name = "New Pack";
    public string Name => m_name;

    [SerializeField] private SkinPackType m_type;
    public SkinPackType Type => m_type;

    [SerializeField] private CustomItemSO[] m_items;
    public CustomItemSO[] Items => m_items;
}
