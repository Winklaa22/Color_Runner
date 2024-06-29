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
    private SkinPackType _currentPack = SkinPackType.NORMAL_PACK;
    public SkinPackType SkinPackType => _currentPack;


    public void OpenDrawScreen(SkinPackType packType)
    {
        _currentPack = packType;
        OnDrawScreenProcessStarted?.Invoke();

    }

    public SkinPack GetCurrentPack()
    {
        SkinPack pack = null;
        switch (_currentPack)
        {
            case SkinPackType.NORMAL_PACK:
                pack = m_normalPack;
                break;
            
            case SkinPackType.RARE_PACK:
                pack = m_rarePack;
                break;
            
            case SkinPackType.ULTIMATE_PACK:
                pack = m_ultimatePack;
                break;

        }

        return pack;
    }
}
