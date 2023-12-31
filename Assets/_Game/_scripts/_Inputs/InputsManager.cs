using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : SceneSingleton<InputsManager>
{
    [SerializeField] private float m_timeForDoubleTap;
    private float _time;
    private int _tapsCount;
    private PlayerControls _playerControls;
    private Vector2 _startedPos;
    private bool _isTouching;
    public Action OnDoubleTapAction;
        
    private void Init()
    {
        _playerControls = new PlayerControls();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Init();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void SetEvents()
    {
        _playerControls.Touch.PrimaryTouchContact.started += context =>  OnStartPrimaryTouch();
        _playerControls.Touch.PrimaryTouchContact.canceled += context => OnEndPrimaryTouch();
    }

    protected override void OnStart()
    {
        base.OnStart();
        SetEvents();
    }

    private void OnStartPrimaryTouch()
    {
        _startedPos = PrimaryTouch();
        _isTouching = true;

        _tapsCount++;
        
        if (_tapsCount >= 2)
        {
            OnDoubleTapAction?.Invoke();
            _tapsCount = 0;
        }
        
        StopCoroutine(ResetTapsCounter());
        StartCoroutine(ResetTapsCounter());

    }

    private IEnumerator ResetTapsCounter()
    {
        yield return new WaitForSeconds(m_timeForDoubleTap);
        _tapsCount = 0;
    }

    private void OnEndPrimaryTouch()
    {
        _isTouching = false;
    }

    private Vector2 PrimaryTouch()
    {
        return _playerControls.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
    }

    public float GetXDirection()
    {
        if (!_isTouching)
            return 0;
            
        var direction = PrimaryTouch() - _startedPos;

        if (direction.Equals(Vector2.zero))
            return 0;
            
        var value = direction.x > 0 ? 1 : -1;
        return value;
    }
}
