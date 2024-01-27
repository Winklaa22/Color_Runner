using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformType
{
    public float probabilityNumber;
    [SerializeField] private GameObject[] m_platforms;
    private List<Platform> _platformsData = new List<Platform>();

    public void Initialize()
    {
        foreach (var platform in m_platforms)
        {
            var p = new Platform(platform);
            Debug.Log(p.ID);
            _platformsData.Add(p);
        }
         
    }

    public Platform GetPlatform()
    {
        var randomIndex = Random.Range(0, _platformsData.Count - 1);
        
        return _platformsData[randomIndex];
    }
}
