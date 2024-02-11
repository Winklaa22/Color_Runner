using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathCollectCoinsPopup : View
{
    [SerializeField] private CoinsCounterController m_coinsCounter;
    [SerializeField] private View m_choiceMenuPopup;

    [Header("Buttons")]
    [SerializeField] private Button m_normalCollectButton;
    [SerializeField] private TMP_Text m_normalCollectButtonText;
    [SerializeField] private Button m_rewardedCollectButton;
    [SerializeField] private TMP_Text m_rewardedCollectButtonText;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_normalCollectButton.onClick.AddListener(OnNormalCollectButtonClicked);
        m_rewardedCollectButton.onClick.AddListener(OnRewardedCollectButtonClicked);
    }

    protected override void OnStart()
    {
        base.OnStart();
        m_coinsCounter.OnFinishedFilling_Entity += delegate
        {
            Invoke(nameof(ChangeScreen), 1.5f);
        };
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        UpdateButtonsText();
    }

    private void UpdateButtonsText()
    {
        var normalCoins = GameManager.Instance.Coins;
        var rewardedCoins = normalCoins + GameManager.Instance.BonusCoins;
        m_normalCollectButtonText.text = m_normalCollectButtonText.text.Replace("$", normalCoins.ToString());
        m_rewardedCollectButtonText.text = m_rewardedCollectButtonText.text.Replace("$", rewardedCoins.ToString());
    }

    public void ChangeScreen()
    {
        m_screenController.CloseLastView();
        m_screenController.OpenPopup(m_choiceMenuPopup);
    }

    public void OnNormalCollectButtonClicked()
    {
        TurnOffButtons();
        m_coinsCounter.UpdateCoinsCount();
    }

    public void OnRewardedCollectButtonClicked()
    {
        TurnOffButtons();
        GameManager.Instance.AddRewaredCoins();
        m_coinsCounter.UpdateCoinsCount();
    }

    private void TurnOffButtons()
    {
        m_normalCollectButton.interactable = false;
        m_rewardedCollectButton.interactable = false;
    }
}
