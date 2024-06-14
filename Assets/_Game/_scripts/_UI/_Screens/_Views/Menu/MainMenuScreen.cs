using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : View
{
    [Header("Buttons")]
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_customPlayerButton;
    [SerializeField] private Button m_rankingButton;
    [SerializeField] private Button m_shopButton;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_playButton.onClick.AddListener(OnPlayButtonClicked);
        m_customPlayerButton.onClick.AddListener(OnCustomPlayerButtonClicked);
        m_shopButton.onClick.AddListener(OnShopButtonClicked);
    }

    private void OnCustomPlayerButtonClicked()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.CUSTOM_PLAYER_SCREEN);
    }

    public void OnShopButtonClicked()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.SHOP_SCREEN);
    }

    private IEnumerator WaitForPlay()
    {
        m_screenController.CloseScreen();
        yield return new WaitForSeconds(GetMaximalDurationOfAnimations());
        SceneTransitionManager.Instance.LoadScene(ScenesNames.GameScene);
    }

    private void OnPlayButtonClicked()
    {
        StartCoroutine(WaitForPlay());
    }
}
