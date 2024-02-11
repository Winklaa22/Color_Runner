using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsManager : SceneSingleton<PlatformsManager>
{
    public List<PlatformType> platformList;
    [SerializeField] private GameObject m_environmentPrefab;
    [SerializeField] private List<GameObject> m_plaforms;
    [SerializeField] private List<EnvironmentController> m_environments;
    [SerializeField] private int _lastNumberOfPlatform = 0;
    [SerializeField] private Platform _lastPlatform;
    [SerializeField] private float m_platformSpeed = 5.0f;
    [SerializeField] private float m_spawningDelay = 3.0f;
    [SerializeField] private int m_platformCount = 5;
    public float PlatformSpeed => m_platformSpeed;

    protected override void OnStart()
    {
        base.OnStart();

        foreach (PlatformType platform in platformList)
        {
            platform.Initialize();
        }

        for (int i = 0; i < m_platformCount; i++)
        {
            SpawnPlatform();
        }
    }
    
    private PlatformType DrawObject(List<PlatformType> platforms)
    {
        float totalProbability = 0f;
        foreach (PlatformType platform in platforms)
        {
            totalProbability += platform.probabilityNumber;
        }
            
        float randomValue = Random.Range(0f, totalProbability);
            
        foreach (PlatformType platform in platforms)
        {
            if (randomValue <= platform.probabilityNumber)
            {
                return platform; 
            }
            randomValue -= platform.probabilityNumber;
        }
            
        return platforms[0];
    }

    public void SpawnPlatform()
    {
        var platformOject = DrawObject(platformList).GetPlatform();

        if(_lastPlatform != null && platformOject.ID.Equals(_lastPlatform.ID))
        {
            while (platformOject.ID.Equals(_lastPlatform.ID))
            {
                platformOject = DrawObject(platformList).GetPlatform();
            }
            
        }

        //Debug.Log("Perfect one: " + platformOject.ID);


        var lastPlatform = m_plaforms[_lastNumberOfPlatform].transform;
        Vector3 position = lastPlatform.position + Vector3.forward * 15;

        var newPlatform = Instantiate(platformOject.Model, position, Quaternion.identity);
        var newEnvironment = Instantiate(m_environmentPrefab, position, Quaternion.identity).GetComponent<EnvironmentController>();
        newEnvironment.SetPlatform(newPlatform.transform);
        m_plaforms.Add(newPlatform);
        m_environments.Add(newEnvironment);
        _lastPlatform = platformOject;
        _lastNumberOfPlatform++;
    }

    public void DestroyPlatform()
    {
        Destroy(m_plaforms[0]);
        Destroy(m_environments[0].gameObject);
        m_environments.RemoveAt(0);
        m_plaforms.RemoveAt(0);
        _lastNumberOfPlatform--;
        SpawnPlatform();
    }
}

// This code was written by Filip Winkler and --------> NATALIA PAWLAK <---------
