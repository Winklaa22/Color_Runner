using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinItem : PickableItem
{
    [SerializeField] private int m_value = 1;

    protected override void Collect()
    {
        base.Collect();
        GameManager.Instance.AddCoins(m_value);
        DestroyCoin();
    }

    private void DestroyCoin()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0, .3f));
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
