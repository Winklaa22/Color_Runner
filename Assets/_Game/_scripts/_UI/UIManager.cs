using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SceneSingleton<UIManager>
{
    [SerializeField] private WarningPopup m_warningPopup;


    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);
    }

    public void ShowWarningPopup(string text)
    {
        m_warningPopup.ShowPopup(text);
    }
}
