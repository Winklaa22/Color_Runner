using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHud : View
{
    [SerializeField] private float m_meters;
    [SerializeField] private TMP_Text m_metersCouter;

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        
    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        Debug.Log("Player Hud has closed");
    }

    private void Update()
    {
        UpdateMetersCounter();
    }

    private void UpdateMetersCounter()
    {
        var meters = GameManager.Instance.Meters;
        m_metersCouter.text = meters < 1000 ? (int)meters + "m" : (meters / 1000).ToString("0.00") + "km";

    }
}
