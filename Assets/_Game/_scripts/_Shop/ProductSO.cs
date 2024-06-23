using System;
using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "newProduct", menuName = "Shop Product")]
public class ProductSO : ScriptableObject
{

    [Button(nameof(GenerateId)), SelfFill(true)]
    public string Id;

    [SerializeField] private string productName;
    public string ProductName => productName;

    [SerializeField] private PaymentType paymentType;
    public PaymentType PaymentType => paymentType;

    [ShowIfIs(nameof(paymentType), PaymentType.VIRTUAL)]
    [SerializeField] private int cost;




    private void GenerateId()
    {
        Id = Guid.NewGuid().ToString();
    }
}
