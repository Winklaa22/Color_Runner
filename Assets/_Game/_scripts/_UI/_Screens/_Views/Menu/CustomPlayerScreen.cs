using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayerScreen : View
{
    [SerializeField] private Button m_backToMenuButton;
    [SerializeField] private TweenAnimator m_menuBackground;

    [Header("Panels")]
    [SerializeField] private TweenAnimator m_malePanel;
    [SerializeField] private CustomPlayerUI m_malePanelController;
    [SerializeField] private TweenAnimator m_femalePanel;
    [SerializeField] private CustomPlayerUI m_femalePanelController;
    private GenderType m_currentPanel;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        m_menuBackground.AnimationOut();
        m_currentPanel = CustomPlayerManager.Instance.Gender;


        if (m_currentPanel == GenderType.MALE)
        {
            m_malePanel.AnimationIn();
            m_malePanelController.RefreshIconsInEachCategory();
        }
        else
        {
            m_femalePanel.AnimationIn();
            m_femalePanelController.RefreshIconsInEachCategory();
        }
    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();

        m_menuBackground.AnimationIn();
        if (m_currentPanel == GenderType.MALE)
        {
            m_malePanel.AnimationOut();
        }
        else
        {
            m_femalePanel.AnimationOut();
        }
            
    }


    private void OnBackToMenuButtonClicked()
    {
        SaveDataManager.Instance.Save();
        ScreensManager.Instance.OpenScreen(ScreenType.MAIN_MENU);
    }

    public void OpenPanel(GenderType genderType)
    {
        m_currentPanel = genderType;
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
