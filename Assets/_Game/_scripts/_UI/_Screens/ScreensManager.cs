using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreensManager : SceneSingleton<ScreensManager>
{
    [SerializeField] private ScreenType m_startScreen = ScreenType.START_SCREEN;
    [SerializeField] private List<ScreenController> m_allScreens;
    [SerializeField] private List<ScreenController> m_screens;

    protected override void OnStart()
    {
        base.OnStart();
        OpenScreen(m_startScreen);
    }

    public void OpenScreen(ScreenController screen)
    {
        if (m_screens.Count > 0)
            CloseScreen(m_screens.Last());

        m_screens.Add(screen);
        screen.OpenScreen();
    }

    public void CloseAllScreens()
    {
        if (m_screens.Count <= 0)
            return;

        foreach(var screen in m_screens)
        {
            screen.CloseScreen();
        }
    }

    public void OpenScreen(ScreenType type)
    {
        var screen = m_allScreens.First(x => x.Type == type);
        OpenScreen(screen);
    }

    public void CloseScreen(ScreenController screen)
    {
        if (m_screens.Count <= 0)
            return;

        screen.CloseScreen();
        m_screens.Remove(screen);
    }

    public void CloseLastScreen()
    {
        CloseScreen(m_screens.Last());
    }
}
