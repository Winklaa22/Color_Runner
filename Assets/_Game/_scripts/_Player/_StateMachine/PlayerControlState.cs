using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControlState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected PlayerControl _player;

    protected PlayerControlState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _player = _stateMachine.Player;
    }

    private void OnTouchEnded()
    {
        if (InputsManager.Instance.GetYDirection() == 1)
        {
            OnSwipeUp();
        }

        if (InputsManager.Instance.GetYDirection() == -1)
        {
            OnSwipeDown();
        }
    }

    protected virtual void OnSwipeUp() { }

    protected virtual void OnSwipeDown() { }

    public virtual void Enter()
    {
        InputsManager.Instance.OnTouchEnd += OnTouchEnded;
        _player.Entity_OnPlayerDied += OnPlayerDied;
    }

    public virtual void Exit()
    {
        InputsManager.Instance.OnTouchEnd -= OnTouchEnded;
        _player.Entity_OnPlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied(DeathType cause, object felonObject = null)
    {
        GameManager.Instance.OnPlayerDie(cause, felonObject);
        _stateMachine.ChangeState(_stateMachine.DeathState);
    }

    public virtual void HandleInput() { }

    public virtual void Update() { }

    public virtual void PhysicsUpdate() { }
}
