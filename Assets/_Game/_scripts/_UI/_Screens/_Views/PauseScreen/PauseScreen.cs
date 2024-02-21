using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : View
{
    [SerializeField] private Button m_returnButton;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_returnButton.onClick.AddListener(ClosePausePanel);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        Time.timeScale = 0;
    }


    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        Time.timeScale = 1;
    }


    private void ClosePausePanel()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.PLAYER_HUD);
    }
}
