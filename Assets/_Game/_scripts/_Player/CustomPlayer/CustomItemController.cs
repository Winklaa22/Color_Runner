using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomItemController : MonoBehaviour
{
    [SerializeField] private CustomItemSO m_itemSO;
    [SerializeField] private GameObject m_itemObject;
    public CustomItemSO ItemSO => m_itemSO;
    public GameObject ItemObject => m_itemObject;

    public CustomItemSO GetItem()
    {
        return m_itemSO;
    }

    public void SetItemActive(bool cond)
    {
        if(m_itemObject != null)
            m_itemObject.SetActive(cond);
    }

    public void SetItemActive(CustomItemSO item, GenderType gender)
    {
        if(item is null)
        {

            return;
        }

        if(m_itemObject != null)
            m_itemObject.SetActive(m_itemSO == item);
    }
}
