using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHud : View
{
    [SerializeField] private float m_meters;
    [SerializeField] private TMP_Text m_metersCouter;
    [SerializeField] private TMP_Text m_coinsCounter;
    [SerializeField] private RectTransform m_coinIcon;

    protected override void OnStart()
    {
        base.OnStart();
        GameManager.Instance.OnCoinsCountChanged_Entity += OnCoinsCollected;
    }

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

    private void OnCoinsCollected(int count)
    {
        m_coinsCounter.text = count.ToString();
        var sequence = DOTween.Sequence();
        sequence.Append(m_coinIcon.DOScale(1.1f, .1f));
        sequence.Append(m_coinIcon.DOScale(1f, .1f));
    }
}
