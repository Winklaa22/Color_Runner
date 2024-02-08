using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGroup : MonoBehaviour
{
    [SerializeField] private float m_gizmosRadius = 1.0f;
    [SerializeField] private Transform[] m_itemsTranform;
    [SerializeField] private Color m_gizmosColor = Color.white;
    private List<PickableItem> _items = new ();

    public void DrawItems()
    {
        foreach (var place in m_itemsTranform)
        {
            var newItemPrefab = ItemsManager.Instance.DrawItem();
            var newItem = Instantiate(newItemPrefab, place);
            newItem.transform.position = place.position;
            _items.Add(newItem);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = m_gizmosColor;
        foreach (var place in m_itemsTranform)
        {
            Gizmos.DrawWireSphere(place.position, m_gizmosRadius);
            
        }
    }
}
