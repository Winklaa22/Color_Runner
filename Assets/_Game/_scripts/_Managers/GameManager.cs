using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{
    [Header("Movement")]
    [SerializeField] private AnimationCurve m_momentumCurve;
    [SerializeField] private float m_curveMask, m_momentumMask;
    [SerializeField] private float m_meters;
    [SerializeField] private float _momentumCurveTime;
    [SerializeField] private bool _isMoving;
    [SerializeField] private TMP_Text m_text; 

    public bool IsMoving
    {
        set => _isMoving = value;
        get => _isMoving;
    }
    public float MomentumMask => m_momentumMask;

    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(CountMeters());
    }
    
    private void Update()
    {
        SetMomentum();
      
        m_text.text = m_meters < 1000 ? (int)m_meters + "m" : (m_meters/1000).ToString("0.00") + "km";
    }

    private void SetMomentum()
    {
        _momentumCurveTime = m_momentumCurve[m_momentumCurve.length - 1].time;
        var mask = _isMoving ? m_curveMask + Time.deltaTime / _momentumCurveTime : m_curveMask - Time.deltaTime / _momentumCurveTime;
        m_curveMask = Mathf.Clamp(mask, 0, 1);
        m_momentumMask = m_momentumCurve.Evaluate(m_curveMask * m_curveMask);
    }

    private IEnumerator CountMeters()
    {
        yield return new WaitForSeconds(.01f);
        m_meters += 0.05f * m_momentumMask;
        yield return CountMeters();
    }

    public void ResetLevel()
    {
        Application.LoadLevel(0);
    }
}
