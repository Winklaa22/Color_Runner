using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayerState : PlayerControlState
{
    public JumpPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.StartCoroutine(Jump());
    }


    private IEnumerator Jump()
    {
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.JumpingBool, true);


        _player.PlayerRigidbody.AddForce(Vector3.up * _player.JumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(_player.JumpAnimationTime - 0.3f);

        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.JumpingBool, false);
        _stateMachine.ChangeState(_stateMachine.RunningState);
    }
}
