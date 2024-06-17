using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : SceneSingleton<DrawManager>
{
    [SerializeField] private SkinPack m_normalPack;
    [SerializeField] private SkinPack m_rarePack;
    [SerializeField] private SkinPack m_ultimatePack;
    public event Action OnDrawScreenProcessStarted;
    private int _currentPack = 1;


    public void OpenDrawScreen(int packIndex)
    {
        _currentPack = packIndex;
        OnDrawScreenProcessStarted?.Invoke();

    }

    public SkinPack GetCurrentPack()
    {
        SkinPack pack = null;
        switch (_currentPack)
        {
            case 1:
                pack = m_normalPack;
                break;
            
            case 2:
                pack = m_rarePack;
                break;
            
            case 3:
                pack = m_ultimatePack;
                break;

        }

        return pack;
    }
}
