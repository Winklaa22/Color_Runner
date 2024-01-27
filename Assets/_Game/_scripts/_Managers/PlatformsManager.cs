using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsManager : SceneSingleton<PlatformsManager>
{
    public List<PlatformType> platformList;
    [SerializeField] private List<GameObject> m_plaforms;
    [SerializeField] private int _lastNumberOfPlatform = 0;
    [SerializeField] private Platform _lastPlatform;
    [SerializeField] private float m_platformSpeed = 5.0f;
    [SerializeField] private float m_spawningDelay = 3.0f;
    public float PlatformSpeed => m_platformSpeed;

    protected override void OnStart()
    {
        base.OnStart();

        foreach (PlatformType platform in platformList)
        {
            platform.Initialize();
        }

        for (int i = 0; i < 5; i++)
        {
            SpawnPlatform();
        }

        StartCoroutine(Spawning());
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
            Debug.Log("Do dupy");
            platformOject = DrawObject(platformList).GetPlatform();
        }

        var lastPlatform = m_plaforms[_lastNumberOfPlatform].transform;
        Vector3 position = lastPlatform.position + Vector3.forward * 15;

        var newPlatform = Instantiate(platformOject.Model, position, Quaternion.identity);
        m_plaforms.Add(newPlatform);
        _lastPlatform = platformOject;
        _lastNumberOfPlatform++;
    }

    public void DestroyPlatform()
    {
        Destroy(m_plaforms[0]);
        m_plaforms.RemoveAt(0);
        _lastNumberOfPlatform--;
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(m_spawningDelay);
        if (!GameManager.Instance.IsMoving)
        {
            while (!GameManager.Instance.IsMoving)
            {
                yield return null;
            }
        }

        SpawnPlatform();
    }

}

// This code was written by Filip Winkler and --------> NATALIA PAWLAK <---------
