using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        
        if (other.CompareTag("Platform"))
        {
            PlatformsManager.Instance.DestroyPlatform();
        }
    }
}
