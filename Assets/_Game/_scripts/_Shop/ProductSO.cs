using System;
using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "newProduct", menuName = "Shop Product")]
public class ProductSO : ScriptableObject
{

    [Button(nameof(GenerateId))]
    [SerializeField] private string id;
    public string ID => id;

    [SerializeField] private string productName = "New Product";
    public string ProductName => productName;

    [SerializeField] private ProductType m_type;
    public ProductType Type => m_type;

    [SerializeField] private PaymentType paymentType;
    public PaymentType PaymentType => paymentType;

    [ShowIfIs(nameof(paymentType), PaymentType.VIRTUAL)]
    [SerializeField] private int cost;
    public int Cost => cost;




    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }
}
