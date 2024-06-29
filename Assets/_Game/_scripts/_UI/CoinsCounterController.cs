using System.Collections;
using TMPro;
using UnityEngine;

public class CoinsCounterController : MonoBehaviour
{
    [SerializeField] private TweenAnimator m_animator;
    [SerializeField] private TMP_Text m_counter;
    [SerializeField] private float m_timeOfFilling = 3.0f;
    public delegate void OnFinishedFilling();
    public OnFinishedFilling Entity_OnFinishedFilling;


    private void Awake()
    {
        ShopManager.Instance.Entity_OnTryToBuyProduct += TryToBuyProduct;
    }



    private void Start()
    {
        UpdateText();
    }

    public void AnimationIn()
    {
        m_animator.AnimationIn();
    }
    public void AnimationOut()
    {
        m_animator.AnimationOut();
    }

    private void UpdateText()
    {
        m_counter.text = PlayerDataManager.Instance.Coins.ToString();
    }

    public void UpdateGameCoinsCount()
    {
        StartCoroutine(UpdateCoinsCounter(GameManager.Instance.Coins));
    }

    public void TryToBuyProduct(ProductSO product)
    {
        if (PlayerDataManager.Instance.Coins >= product.Cost)
        {
            BuyProduct(product);
            
        }
        else
        {
            UIManager.Instance.ShowWarningPopup(WarningMessagesConst.NotEnoghtCoinsMessage);
        }
    }

    private void BuyProduct(ProductSO product)
    {

        StartCoroutine(UpdateCoinsCounter(-product.Cost));

        Entity_OnFinishedFilling += () =>
        {
            ShopManager.Instance.ProductHasBought(product);
            PlayerDataManager.Instance.SubtractCoins(product.Cost);
        };
    }


    public IEnumerator UpdateCoinsCounter(int count)
    {
        float elapsed = 0.0f;
        var actualCount = PlayerDataManager.Instance.Coins;
        while (elapsed < m_timeOfFilling)
        {
            elapsed += Time.deltaTime;
            int currentValue = (int)Mathf.Lerp(actualCount, actualCount + count, elapsed / m_timeOfFilling); 
            m_counter.text = currentValue.ToString();
            yield return null;
        }


        Entity_OnFinishedFilling?.Invoke();
 
    }
    private void OnDestroy()
    {
        ShopManager.Instance.Entity_OnTryToBuyProduct -= TryToBuyProduct;

        StopAllCoroutines();
    }

}
