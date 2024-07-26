using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePlayerState : PlayerControlState
{
    public SlidePlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.StartCoroutine(Slide());
    }

    protected override void OnSwipeUp()
    {
        base.OnSwipeUp();
        EndSlide();
    }

    private IEnumerator Slide()
    {
        _player.CanMove = false;
        _player.CameraTranform.DOLocalMove(_player.CameraSlidePosition, _player.SlideCameraAnimDuration);
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.SlidingBool, true);
        yield return new WaitForSeconds(_player.SlidingDuration);
        _stateMachine.ChangeState(_stateMachine.RunningState);
    }

    private void EndSlide()
    {
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.SlidingBool, false);
        _player.CameraTranform.DOLocalMove(_player.CameraPrimatyPosition, _player.SlideCameraAnimDuration);
        
    }

    public override void Exit()
    {
        base.Exit();
        EndSlide();
    }
}
