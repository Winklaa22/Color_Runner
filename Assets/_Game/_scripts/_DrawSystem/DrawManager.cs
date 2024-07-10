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
    [SerializeField] private SkinPackType _currentPack = SkinPackType.NORMAL_PACK;
    public SkinPackType SkinPackType => _currentPack;


    public void OpenDrawScreen(SkinPackType packType)
    {
        _currentPack = packType;
        OnDrawScreenProcessStarted?.Invoke();

    }

    public SkinPack GetCurrentPack()
    {
        return _currentPack switch
        {
            SkinPackType.NORMAL_PACK => m_normalPack,
            SkinPackType.RARE_PACK => m_rarePack,
            SkinPackType.ULTIMATE_PACK => m_ultimatePack,
            _ => throw new NotImplementedException(),
        };
    }
}
