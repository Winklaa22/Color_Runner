using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private Transform m_platform;
    public void SetPlatform(Transform platform)
    {
        m_platform = platform;
    }

    private void Update()
    {
        if(m_platform != null){
            transform.position = m_platform.position;
        }
    }
}
