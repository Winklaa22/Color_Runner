using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : SceneSingleton<InputsManager>
{
    private PlayerControls _playerControls;
    private Vector2 _startedPos;
    private bool _isTouching;
        
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

    private void Start()
    {
        SetEvents();
    }

    private void OnStartPrimaryTouch()
    {
        _startedPos = PrimaryTouch();
        _isTouching = true;

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
