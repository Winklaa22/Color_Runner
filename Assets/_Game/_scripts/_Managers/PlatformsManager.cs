using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsManager : SceneSingleton<PlatformsManager>
{
    public List<Platform> platformList;
    [SerializeField] private List<GameObject> m_plaforms;
    [SerializeField] private int _lastNumberOfPlatform = 0;
    [SerializeField] private float m_platformSpeed = 5.0f;
    public float PlatformSpeed => m_platformSpeed;

    protected override void OnStart()
    {
        base.OnStart();

        for (int i = 0; i < 5; i++)
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        var platformOject = DrawObject(platformList).gameObject;

        var lastPlatform = m_plaforms[_lastNumberOfPlatform].transform;
        Vector3 position = lastPlatform.position + Vector3.forward * lastPlatform.localScale.z;

        var newPlatform = Instantiate(platformOject, position, Quaternion.identity);
        m_plaforms.Add(newPlatform);
        _lastNumberOfPlatform++;
    }

    public void DestroyPlatform()
    {
        Destroy(m_plaforms[0]);
        m_plaforms.RemoveAt(0);
        _lastNumberOfPlatform--;
    }

    private Platform DrawObject(List<Platform> platforms)
    {
        float totalProbability = 0f;
        foreach (Platform platform in platforms)
        {
            totalProbability += platform.probabilityNumber;
        }
        
        float randomValue = Random.Range(0f, totalProbability);
        
        foreach (Platform platform in platforms)
        {
            if (randomValue <= platform.probabilityNumber)
            {
                return platform; 
            }
            randomValue -= platform.probabilityNumber;
        }
        
        return platforms[0];
    }
}
[System.Serializable]
public class Platform
{
    public string name;
    public float probabilityNumber;
    public GameObject gameObject; 
}
