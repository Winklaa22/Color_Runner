using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private float _timeToDestroy = 5.0f;

    public IEnumerator CountingToDestroy()
    {
        PlatformsManager.Instance.SpawnPlatform();
        yield return new WaitForSeconds(_timeToDestroy);
        PlatformsManager.Instance.DestroyPlatform();
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * 5);
    }
}
