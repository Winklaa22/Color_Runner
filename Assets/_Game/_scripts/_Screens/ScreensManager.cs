using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreensManager : SceneSingleton<ScreensManager>
{
    [SerializeField] private List<ScreenController> m_screens;

    public void OpenScreen(ScreenController screen)
    {
        if(m_screens.Count > 0)
            screen.CloseScreen();

        m_screens.Add(screen);
    }
    
    public void OpenScreen(ScreenType type)
    {
        if(m_screens.Count <= 0)
        {
            Debug.LogError("There is no screens to open");
            return;
        }

        var screen = m_screens.First(x => x.Type == type);
        OpenScreen(screen);
    }

    public void CloseScreen(ScreenController screen)
    {
        if (m_screens.Count <= 0)
            return;

        screen.CloseScreen();
    }
}
