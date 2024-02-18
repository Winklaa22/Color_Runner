using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "customItem", menuName = "Custom Item")]
public class CustomItemSO : ScriptableObject
{
    [SerializeField] private Sprite m_icon;
    [SerializeField] private string m_name;
    [SerializeField] private CustomItemType m_type;
    [SerializeField] private bool m_isDefault;
    public string ID => $"{m_type}_{m_name}";
    public Sprite Icon => m_icon;
    public CustomItemType Type => m_type;
    public bool IsDefault => m_isDefault;
}
