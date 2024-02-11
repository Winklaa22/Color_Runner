using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour
{
    [SerializeField] protected PickableItemType m_itemType;
    protected bool _isCollected;

    protected virtual void Collect()
    {
        _isCollected = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _isCollected)
            return;

        Collect();
    }
}
