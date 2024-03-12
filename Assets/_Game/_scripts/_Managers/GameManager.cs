using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SceneSingleton<GameManager>
{
    [SerializeField] private PlayerController m_player;
    public PlayerController Player => m_player;

    [Header("Movement")]
    [SerializeField] private AnimationCurve m_momentumCurve;
    [SerializeField] private float m_curveMask, m_momentumMask;
    [SerializeField] private float m_meters;
    public float Meters => m_meters;
    [SerializeField] private float _momentumCurveTime;
    [SerializeField] private bool _isMoving;

    [Header("Coins")]
    [SerializeField] private float m_rewardedPercent = 15;
    [SerializeField] private int _coins;
    public int Coins => _coins;
    public int BonusCoins => (int)(_coins * (m_rewardedPercent / 100));
    public delegate void OnCoinsCountChanged(int count);
    public OnCoinsCountChanged OnCoinsCountChanged_Entity;



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

    public void AddRewaredCoins()
    {
        _coins += BonusCoins; 
    }

    internal void StartGame()
    {
        _isMoving = true;
    }

    private void Update()
    {
        SetMomentum();
    }

    public void AddCoins(int count)
    {
        _coins += count;
        OnCoinsCountChanged_Entity?.Invoke(_coins);
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

    public void GameOver()
    {
        _isMoving = false;
    }

    public void CollectCoins()
    {
        PlayerDataManager.Instance.AddCoins(_coins);
        _coins = 0;
        SaveDataManager.Instance.Save();
    }

    public void ResetLevel()
    {
        SceneTransitionManager.Instance.LoadScene(ScenesNames.GameScene);
    }
}
