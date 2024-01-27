using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{
    [Header("Movement")]
    [SerializeField] private AnimationCurve m_momentumCurve;
    [SerializeField] private float m_curveMask, m_momentumMask;
    
    [SerializeField] private float _momentumCurveTime;
    [SerializeField] private bool _isMoving;

    public bool IsMoving
    {
        set => _isMoving = value;
        get => _isMoving;
    }
    public float MomentumMask => m_momentumMask;

    private void Update()
    {
        SetMomentum();
    }

    private void SetMomentum()
    {
        _momentumCurveTime = m_momentumCurve[m_momentumCurve.length - 1].time;
        var mask = _isMoving ? m_curveMask + Time.deltaTime / _momentumCurveTime : m_curveMask - Time.deltaTime / _momentumCurveTime;
        m_curveMask = Mathf.Clamp(mask, 0, 1);
        m_momentumMask = m_momentumCurve.Evaluate(m_curveMask * m_curveMask);
    }

    public void ResetLevel()
    {
        Application.LoadLevel(0);
    }
}
