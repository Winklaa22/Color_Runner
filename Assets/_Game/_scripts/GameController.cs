using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.Entity_OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        StartCoroutine(GameOverProcess());
    }

    private IEnumerator GameOverProcess()
    {
        ScreensManager.Instance.CloseLastScreen();

        yield return new WaitForSeconds(2f);
        ScreensManager.Instance.OpenScreen(ScreenType.DEATH_SCREEN);
    }
}
