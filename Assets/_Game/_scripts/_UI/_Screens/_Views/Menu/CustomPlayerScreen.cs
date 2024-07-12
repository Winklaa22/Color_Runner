using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayerScreen : View
{
    [Header("Character")]
    [SerializeField] private Transform m_characterTranform;
    [SerializeField] private float m_characterAnimationDuration;
    [SerializeField] private ParticleSystem m_effect;

    [SerializeField] private Button m_backToMenuButton;
    [SerializeField] private TweenAnimator m_menuBackground;

    [Header("Panels")]
    [SerializeField] private NavigationController m_genderNavigation;
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

    public void ChangeGender(GenderType genderType)
    {
        StartCoroutine(ChangingGenderProcess(genderType));
    }


    private IEnumerator ChangingGenderProcess(GenderType genderType)
    {
        ClosePanel();
        m_genderNavigation.IsInteractable = false;

        var rot = m_characterTranform.rotation.eulerAngles;
        m_characterTranform.DOLocalRotate(new Vector3(rot.x, rot.y + 360, rot.z), m_characterAnimationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.InExpo)
            .OnComplete(() =>
            {
                
                CustomPlayerManager.Instance.ChangeGender(genderType);
                OpenPanel(genderType);
                m_genderNavigation.IsInteractable = true; ;
            });

        
        yield return new WaitForSeconds(m_characterAnimationDuration / 1.3f);
        m_effect.Play();
    }

    public void ChangePanel(GenderType genderType)
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

    private void OpenPanel(GenderType genderType)
    {
        m_currentPanel = genderType;
        switch (genderType)
        {
            case GenderType.MALE:
                m_malePanel.AnimationIn();
                break;
            case GenderType.FEMALE:
                m_femalePanel.AnimationIn();
                break;
        }
    }

    private void ClosePanel()
    {
        switch (m_currentPanel)
        {
            case GenderType.MALE:
                m_malePanel.AnimationOut();
                break;
            case GenderType.FEMALE:
                m_femalePanel.AnimationOut();
                break;
        }
    }
}
