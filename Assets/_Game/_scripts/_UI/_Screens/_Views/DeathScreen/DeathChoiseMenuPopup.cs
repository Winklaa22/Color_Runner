using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathChoiseMenuPopup : View
{
    [SerializeField] private Button m_playAgainButton;
    [SerializeField] private Button m_returnToMenuButton;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_playAgainButton.onClick.AddListener(OnPlayAgainButton);
        m_returnToMenuButton.onClick.AddListener(OnReturnToMenuButton);
    }

    private void OnPlayAgainButton()
    {
        StartCoroutine(WaitForPlayAgain());
    }

    private IEnumerator WaitForPlayAgain()
    {
        m_screenController.CloseScreen();
        yield return new WaitForSeconds(GetMaximalDurationOfAnimations());
        SceneManager.LoadScene("Game");
    }

    private void OnReturnToMenuButton()
    {
        StartCoroutine(WaitForReturnToMenu());
    }

    private IEnumerator WaitForReturnToMenu()
    {
        m_screenController.CloseScreen();
        yield return new WaitForSeconds(GetMaximalDurationOfAnimations());
        SceneManager.LoadScene("Menu");
    }
}
