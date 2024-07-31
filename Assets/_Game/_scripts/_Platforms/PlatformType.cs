using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlatformType   
{
    [SerializeField] private int m_index;
    public int Index => m_index;
    [SerializeField] private GameObject[] m_platforms;

    public GameObject GetPlatform()
    {
        var randomIndex = Random.Range(0, m_platforms.Length - 1);
        
        return m_platforms[randomIndex];
    }
}
