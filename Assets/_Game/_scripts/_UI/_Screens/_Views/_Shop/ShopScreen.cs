using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : View
{
    [SerializeField] private Button m_backToMenuButton;
    [SerializeField] private Button m_buyButton;
    [Header("Skin Packs")]
    [SerializeField] private ScrollSwipeHandler m_skinPackSwipeHandler;
    [SerializeField] private TweenAnimator m_skinPackBuyButton;
    private SkinPackType _currentSkinPack;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_backToMenuButton.onClick.AddListener(BackToMenu);
        m_buyButton.onClick.AddListener(OnBuyButtonClicked);

        m_skinPackSwipeHandler.OnSwipingStart += delegate
        {
            m_skinPackBuyButton.AnimationOut();
        };

        m_skinPackSwipeHandler.Entity_OnPageChanged += OnPageChanged;
        
    }

    private void OnProductHasBought(ProductSO product)
    {
        DrawManager.Instance.OpenDrawScreen(_currentSkinPack);
        ScreensManager.Instance.OpenScreen(ScreenType.DRAW_SCREEN);
        
    }

    public void OnBuyButtonClicked()
    {
        ShopManager.Instance.BuyProduct(ShopManager.Instance.GetSkinPackProduct(_currentSkinPack));
    }

    private void OnPageChanged(int pageIndex)
    {
        m_skinPackBuyButton.AnimationIn();
        _currentSkinPack = (SkinPackType)(pageIndex - 1);
    }

    private void BackToMenu()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.MAIN_MENU);
        MainMenuManager.Instance.SetCoinsCounterActive(false);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        MainMenuManager.Instance.SetCoinsCounterActive(true);
        ShopManager.Instance.Entity_OnVirtualProductHasBought += OnProductHasBought;
    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        ShopManager.Instance.Entity_OnVirtualProductHasBought -= OnProductHasBought;
    }
}
