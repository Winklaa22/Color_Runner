using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.TryGetComponent(out PlatformController obj))
        {
            StartCoroutine(obj.CountingToDestroy());
        }
            
    }
}
