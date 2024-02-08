using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGroup : MonoBehaviour
{
    [SerializeField] private float m_gizmosRadius = 1.0f;
    [SerializeField] private Transform[] m_itemsTranform;
    private List<PickableItem> _items;

    public void DrawItems()
    {
        foreach (var place in m_itemsTranform)
        {
            var newItemPrefab = ItemsManager.Instance.DrawItem();
            var newItem = Instantiate(newItemPrefab, place.position, place.rotation);
            _items.Add(newItem);
        }
    }

    private void OnDrawGizmos()
    {
        foreach(var place in m_itemsTranform)
        {
            Gizmos.DrawWireSphere(place.position, m_gizmosRadius);
        }
    }
}
