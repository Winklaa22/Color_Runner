using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private ItemsGroup[] m_itemsGroups;

    private void Start()
    {
        DrawGroups();
    }

    private void DrawGroups()
    {
        var randomIndex = Random.Range(0, m_itemsGroups.Length - 1);
        m_itemsGroups[randomIndex].DrawItems();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * GetSpeed());
    }

    private float GetSpeed()
    {

        return PlatformsManager.Instance.PlatformSpeed * GameManager.Instance.MomentumMask;
    }


}
