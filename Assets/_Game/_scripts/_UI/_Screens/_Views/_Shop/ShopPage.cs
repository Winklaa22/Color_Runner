using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ShopPage : MonoBehaviour
{
    [SerializeField] protected ProductSO m_product;
    [SerializeField] private TMP_Text m_headerText;
    [SerializeField] private TMP_Text m_priceText;

    private void Start()
    {
        m_headerText.text = m_product.ProductName;
        m_priceText.text = $"{m_product.Cost}{UIManager.Instance.GetResouceIcon(IconConsts.CoinIcon)}";
    }
}
