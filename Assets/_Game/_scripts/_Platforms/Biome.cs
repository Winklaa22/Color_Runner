using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Biome
{
    [SerializeField] private Vector2 m_distanceRange;
    public float MaxDistance => m_distanceRange.y;
    [SerializeField] private EnvironmentController[] m_environmentPrefabs;
    public EnvironmentController[] EnvironmentPrefabs => m_environmentPrefabs;
    [SerializeField] private List<PlatformType> m_platformTypes;
    public List<PlatformType> PlatformTypes => m_platformTypes;

    public bool IsDistanceMatch()
    {
        var distance = GameManager.Instance.Meters;

        return distance >= m_distanceRange.x && distance <= m_distanceRange.y; 
    }
}
