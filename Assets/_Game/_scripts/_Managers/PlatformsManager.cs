using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsManager : SceneSingleton<PlatformsManager>
{
    [Header("Prefabs")]
    [SerializeField] private List<PlatformType> m_platformTypes;
    [SerializeField] private GameObject[] m_environmentPrefabs;
    private int _lastTypeIndex;
    
    [SerializeField] private List<EnvironmentController> _environments;
    [SerializeField] private List<GameObject> _platforms;
    [SerializeField] private int _lastNumberOfPlatform = 0;
    [SerializeField] private Platform _lastPlatform = null;

    [Header("Properties")]
    [SerializeField] private float m_platformSpeed = 5.0f;
    [SerializeField] private float m_spawningDelay = 3.0f;
    [SerializeField] private int m_platformCount = 5;
    public float PlatformSpeed => m_platformSpeed;

    protected override void OnStart()
    {
        base.OnStart();

        m_platformTypes.ForEach(x => { x.Init(); });

        for (int i = 0; i < m_platformCount; i++)
        {
            SpawnPlatform();
        }
    }
    private PlatformType DrawPlatformType()
    {
        var platformType = Random.Range(0, m_platformTypes.Count);

        while (m_platformTypes[platformType].Index.Equals(_lastTypeIndex))
        {
            platformType = Random.Range(0, m_platformTypes.Count);
        }

        return m_platformTypes[platformType];

    }

    public void SpawnPlatform()
    {
        var platformType = DrawPlatformType();
        var position = _platforms[_lastNumberOfPlatform].transform.position + Vector3.forward * 15;
        var platform = platformType.DrawPlatform(_lastPlatform);
        var newPlatform = Instantiate(platform.Model, position, Quaternion.identity);
        var newEnvironment = Instantiate(m_environmentPrefabs[Random.Range(0, m_environmentPrefabs.Length - 1)], position, Quaternion.identity).GetComponent<EnvironmentController>();
        newEnvironment.SetPlatform(newPlatform.transform);
        _platforms.Add(newPlatform);
        _environments.Add(newEnvironment);
        _lastPlatform = platform;
        _lastNumberOfPlatform++;
    }

    public void DestroyPlatform()
    {
        Destroy(_platforms[0]);
        Destroy(_environments[0].gameObject);
        _environments.RemoveAt(0);
        _platforms.RemoveAt(0);
        _lastNumberOfPlatform--;
        SpawnPlatform();
    }
}

// This code was written by Filip Winkler and --------> NATALIA PAWLAK <---------
