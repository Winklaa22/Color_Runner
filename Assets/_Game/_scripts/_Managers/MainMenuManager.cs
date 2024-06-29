using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : SceneSingleton<MainMenuManager>
{
    [SerializeField] private CoinsCounterController m_coinsCounter;
    public CoinsCounterController CoinsCounter => m_coinsCounter;

    public void SetCoinsCounterActive(bool active)
    {
        if (active)
            m_coinsCounter.AnimationIn();
        else
            m_coinsCounter.AnimationOut();
    }
}
