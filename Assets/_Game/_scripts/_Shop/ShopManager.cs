using System.Linq;
using UnityEngine;

public class ShopManager : SceneSingleton<ShopManager>
{
    [SerializeField] private ProductSO[] m_allProducts;

    public delegate void OnTryToBuyProduct(ProductSO product);
    public event OnTryToBuyProduct Entity_OnTryToBuyProduct;

    public delegate void OnVirtualProductHasBought(ProductSO product);
    public event OnVirtualProductHasBought Entity_OnVirtualProductHasBought;


    public void BuyProduct(ProductType type)
    {
        var product = m_allProducts.First(x => x.Type == type);

        switch (product.PaymentType)
        {
            case PaymentType.VIRTUAL:
                BuyVirtualProduct(product);
                break;


            case PaymentType.INAPP:
                BuyInappProduct(product);
                break;
        }
    }

    public ProductType GetSkinPackProduct(SkinPackType skinPackType)
    {
        var productType = skinPackType switch
        {
            SkinPackType.NORMAL_PACK => ProductType.NORMAL_DRAW_PACK,
            SkinPackType.RARE_PACK => ProductType.RARE_DRAW_PACK,
            SkinPackType.ULTIMATE_PACK => ProductType.ULTIMATE_DRAW_PACK,
            _ => throw new System.NotImplementedException(),
        };

        return productType;
    }

    public ProductSO GetProduct(ProductType type)
    {
        return m_allProducts.First(x => x.Type == type);
    }

    private void BuyVirtualProduct(ProductSO product)
    {

        Entity_OnTryToBuyProduct?.Invoke(product);
    
    }

    private void BuyInappProduct(ProductSO product)
    {

    }

    public void ProductHasBought(ProductSO product)
    {
        Entity_OnVirtualProductHasBought?.Invoke(product);
    }


}
