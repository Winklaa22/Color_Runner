using System.Collections;
using TMPro;
using UnityEngine;

public class CoinsCounterController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_counter;
    [SerializeField] private float m_timeOfFilling = 3.0f;
    public delegate void OnFinishedFilling();
    public OnFinishedFilling OnFinishedFilling_Entity;


    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        m_counter.text = PlayerDataManager.Instance.Coins.ToString();
    }

    public void UpdateCoinsCount()
    {
        StartCoroutine(UpdateCoinsCounter(GameManager.Instance.Coins));
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


        GameManager.Instance.CollectCoins();
        OnFinishedFilling_Entity?.Invoke();
    }


}
