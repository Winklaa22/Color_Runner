using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "customItem", menuName = "Custom Item")]
public class CustomItemSO : ScriptableObject
{
    [SerializeField] private Sprite m_maleIcon;
    [SerializeField] private Sprite m_femaleIcon;
    [SerializeField] private string m_name;
    [SerializeField] private CustomItemType m_type;
    [SerializeField] private bool m_nonUnisex;

    [ShowIf(nameof(m_nonUnisex))]
    [SerializeField] private GenderType m_gender;

    [SerializeField] private bool m_isDefault;
    public string ID => $"{m_type}_{m_name}";
    public Sprite MaleIcon => m_maleIcon;
    public Sprite FemaleIcon => m_femaleIcon;
    public CustomItemType Type => m_type;
    public bool IsDefault => m_isDefault;

    public bool CheckGender(GenderType currGender)
    {
        return m_nonUnisex && currGender != m_gender;
    }
}
