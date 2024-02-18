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

    public void SetItemActive(CustomItemSO item)
    {
        if(item is null || (m_itemSO.IsDefault && item.IsDefault))
        {
            if(item.Type.Equals(CustomItemType.SKIN))
                m_itemObject.SetActive(true);
            return;
        }

        if(m_itemObject != null)
            m_itemObject.SetActive(m_itemSO == item);
    }
}
