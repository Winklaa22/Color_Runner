using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayerScreen : View
{
    [SerializeField] private Button m_backToMenuButton;
    [SerializeField] private TweenAnimator m_menuBackground;

    [Header("Panels")]
    [SerializeField] private TweenAnimator m_malePanel;
    [SerializeField] private TweenAnimator m_femalePanel;


    protected override void OnAwake()
    {
        base.OnAwake();
        m_backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        m_menuBackground.AnimationOut();
        OpenPanel(CustomPlayerManager.Instance.Gender);
    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        m_menuBackground.AnimationIn();

    }

    private void OnBackToMenuButtonClicked()
    {
        SaveDataManager.Instance.Save();
        ScreensManager.Instance.OpenScreen(ScreenType.MAIN_MENU);
    }

    public void OpenPanel(GenderType genderType)
    {
        switch (genderType)
        {
            case GenderType.MALE:
                m_malePanel.AnimationIn();
                m_femalePanel.AnimationOut();
                break;
            case GenderType.FEMALE:
                m_femalePanel.AnimationIn();
                m_malePanel.AnimationOut();
                break;
        }
    }
}
