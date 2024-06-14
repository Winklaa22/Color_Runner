using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : View
{
    [SerializeField] private Button m_backToMenuButton;
    [Header("Skin Packs")]
    [SerializeField] private ScrollSwipeHandler m_skinPackSwipeHandler;
    [SerializeField] private TweenAnimator m_skinPackBuyButton;

    protected override void OnAwake()
    {
        base.OnAwake();

        m_backToMenuButton.onClick.AddListener(BackToMenu);

        m_skinPackSwipeHandler.OnSwipingStart += delegate
        {
            m_skinPackBuyButton.AnimationOut();
        };

        m_skinPackSwipeHandler.Entity_OnPageChanged += delegate(int i)
        {
            m_skinPackBuyButton.AnimationIn();
        };
    }

    private void BackToMenu()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.MAIN_MENU);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
    }
}
