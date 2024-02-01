using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private float _timeToDestroy = 5.0f;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * GetSpeed());
    }

    private float GetSpeed()
    {

        return PlatformsManager.Instance.PlatformSpeed * GameManager.Instance.MomentumMask;
    }


}
