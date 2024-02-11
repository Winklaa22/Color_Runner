using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayerScreen : View
{
    [SerializeField] private Button m_backToMenuButton;
    [SerializeField] private TweenAnimator m_menuBackground;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        m_menuBackground.AnimationOut();
    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        m_menuBackground.AnimationIn();

    }

    private void OnBackToMenuButtonClicked()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.MAIN_MENU);
    }
}
