using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurvedWorldController : MonoBehaviour
{
    [SerializeField] private float m_horizontalValue;
    [SerializeField] private float m_verticalValue;

    private void Start()
    {
        Shader.SetGlobalFloat("_VerticalCurve", m_verticalValue / 10000);
        Shader.SetGlobalFloat("_HorizontalCurve", m_horizontalValue / 10000);
    }

    private void Update()
    {
        if (Application.isPlaying)
            return;

        Shader.SetGlobalFloat("_VerticalCurve", m_verticalValue / 10000);
        Shader.SetGlobalFloat("_HorizontalCurve", m_horizontalValue / 10000);

    }
}
