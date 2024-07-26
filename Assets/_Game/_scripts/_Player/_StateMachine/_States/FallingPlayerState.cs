using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlayerState : PlayerControlState
{
    public FallingPlayerState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        base.Update();

        if (_player.IsGrounded())
        {
            _stateMachine.ChangeState(_stateMachine.RunningState);
        }
    }

    public override void Enter()
    {
        base.Enter();
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.FallingBool, true);
    }

    public override void Exit()
    {
        base.Exit();
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.FallingBool, true);
    }
}
