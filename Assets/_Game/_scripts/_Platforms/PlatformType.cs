using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlatformType   
{
    [SerializeField] private int m_index;
    public int Index => m_index;
    [SerializeField] private Platform[] m_platforms;

    public void Init()
    {
        m_platforms.ToList().ForEach(x =>
        {
            x.ID = System.Guid.NewGuid().ToString();
        });
    }

    public Platform GetPlatform()
    {
        var randomIndex = Random.Range(0, m_platforms.Length - 1);
        
        return m_platforms[randomIndex];
    }

    public Platform DrawPlatform(Platform lastPlatform)
    {
        float totalProbability = 0f;
        foreach (var platform in m_platforms)
        {
            totalProbability += platform.ProbabilityNumber;
        }

        float randomValue = Random.Range(0f, totalProbability);

        foreach (var platform in m_platforms)
        {
            if (randomValue <= platform.ProbabilityNumber)
            {
                if (lastPlatform != null && platform.ID.Equals(lastPlatform.ID))
                    continue;

                return platform;
            }
            randomValue -= platform.ProbabilityNumber;
        }

        return m_platforms[0];
    }
}
