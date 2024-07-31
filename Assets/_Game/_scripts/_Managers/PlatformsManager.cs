using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformsManager : SceneSingleton<PlatformsManager>
{
    [Header("Prefabs")]
    [SerializeField] private Biome[] m_biomes;
    private Biome _currentBiome;
    [SerializeField] private int _lastTypeIndex;
    
    [SerializeField] private List<EnvironmentController> _environments;
    [SerializeField] private List<GameObject> _platforms;
    [SerializeField] private int _lastNumberOfPlatform = 0;
    [SerializeField] private GameObject _lastPlatform = null;
    [SerializeField]  private int _indexToSpawn;

    [Header("Properties")]
    [SerializeField] private float m_platformSpeed = 5.0f;
    [SerializeField] private float m_spawningDelay = 3.0f;
    [SerializeField] private int m_platformCount = 5;
    public float PlatformSpeed => m_platformSpeed;

    protected override void OnStart()
    {
        base.OnStart();

        _indexToSpawn = Random.Range(0, GetCurrentBiome().PlatformTypes.Count - 1);
        for (int i = 0; i < m_platformCount; i++)
        {
            SpawnPlatform();
        }
    }

    private Biome GetCurrentBiome()
    {
        if (m_biomes[m_biomes.Length - 1].MaxDistance < GameManager.Instance.Meters)
        {
            return m_biomes[m_biomes.Length - 1];
        }
            

        return m_biomes.First(x => x.IsDistanceMatch());
    }

    private void SpawnPlatform()
    {
        var platformType = GetCurrentBiome().PlatformTypes[_indexToSpawn];
        var position = _platforms[_lastNumberOfPlatform].transform.position + Vector3.forward * 15;
        var platform = platformType.GetPlatform();
        var newPlatform = Instantiate(platform, position, Quaternion.identity);
        _platforms.Add(newPlatform);
        _lastPlatform = platform;
        _lastNumberOfPlatform++;
        _indexToSpawn = _indexToSpawn < GetCurrentBiome().PlatformTypes.Count - 1 ? _indexToSpawn + 1 : 0;
        SpawnEnvironment(newPlatform.transform, position);

        
    }

    private void SpawnEnvironment(Transform newPlatform, Vector3 pos)
    {
        var prefabs = GetCurrentBiome().EnvironmentPrefabs;
        var newEnvironment = Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], pos, Quaternion.identity).GetComponent<EnvironmentController>();
        newEnvironment.SetPlatform(newPlatform);
        _environments.Add(newEnvironment);
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
