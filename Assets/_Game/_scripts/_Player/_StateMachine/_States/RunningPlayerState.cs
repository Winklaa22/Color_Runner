using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningPlayerState : PlayerControlState
{
    public RunningPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.CanMove = true;
        _player.PlayerCollider.ChangeState(PlayersColliderState.RUNNING);
    }

    public override void Update()
    {
        base.Update();

        if (!_player.IsGrounded())
        {
            _stateMachine.ChangeState(_stateMachine.FallingState);
        }

    }

    protected override void OnSwipeUp()
    {
        base.OnSwipeUp();
        _stateMachine.ChangeState(_stateMachine.JumpState);
    }

    protected override void OnSwipeDown()
    {
        base.OnSwipeDown();
        _stateMachine.ChangeState(_stateMachine.SlideState);
    }
}
