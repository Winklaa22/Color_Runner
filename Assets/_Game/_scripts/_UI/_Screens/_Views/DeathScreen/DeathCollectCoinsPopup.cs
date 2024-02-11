using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCollectCoinsPopup : View
{
    [SerializeField] private CoinsCounterController m_coinsCounter;
    [SerializeField] private View m_choiceMenuPopup;

    [Header("Buttons")]
    [SerializeField] private Button m_normalCollectButton;
    [SerializeField] private Button m_rewardedCollectButton;

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
