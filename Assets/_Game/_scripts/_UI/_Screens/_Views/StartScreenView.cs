using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScreenView : View
{
    [SerializeField] private EventTrigger m_maskEvent;
    private bool _isStarted;

    protected override void OnAwake()
    {
        base.OnAwake();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnStartGame(); });
        m_maskEvent.triggers.Add(entry);
    }

    private void OnStartGame()
    {
        if (_isStarted)
            return;

        GameManager.Instance.StartGame();
        ScreensManager.Instance.OpenScreen(ScreenType.PLAYER_HUD);
        _isStarted = true;
    }
}
