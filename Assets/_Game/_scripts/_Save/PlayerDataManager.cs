using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataManager : SceneSingleton<PlayerDataManager>, ISaveable
{
    private int _coins;
    public int Coins => _coins;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(Instance.gameObject);
    }

    protected override void OnStart()
    {
        base.OnStart();

    }

    public void AddCoins(int coins)
    {
        _coins += coins;
    }


    public object CaptureState()
    {
        return new SaveData
        {
            CoinsData = _coins
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        _coins = saveData.CoinsData;
    }

    [System.Serializable]
    private struct SaveData
    {
        public int CoinsData;
    }
}
