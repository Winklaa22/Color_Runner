using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : SceneSingleton<CoinsManager>
{
    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);
    }


}
