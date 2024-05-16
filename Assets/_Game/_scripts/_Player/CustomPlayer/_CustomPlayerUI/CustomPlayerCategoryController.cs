using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerCategoryController : MonoBehaviour
{
    [SerializeField] private CustomItemSO[] m_items;
    [SerializeField] private CustomItemIcon m_iconPrefab;
    [SerializeField] private Transform m_content;
    [SerializeField] private GenderType m_gender;

    private void Start()
    {
        AddIcons();
    }

    private void AddIcons()
    {
        foreach(var item in m_items)
        {
            var icon = Instantiate(m_iconPrefab, m_content);
            icon.SetIcon(item, m_gender);
        }
    }
}
