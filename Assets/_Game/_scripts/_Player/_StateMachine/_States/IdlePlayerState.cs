using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerControlState
{
    public IdlePlayerState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerCollider.ChangeState(PlayersColliderState.IDLE);
    }
}
