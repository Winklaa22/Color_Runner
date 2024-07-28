using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayerState : PlayerControlState
{
    public DeathPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player.CameraTranform.DOLocalMove(_player.DeathCameraPosition, 2).SetEase(Ease.InOutExpo);
        _player.PlayerAnimator.enabled = false;
        _player.PlayerCollider.enabled = false;
        _player.PlayerRigidbody.isKinematic = true;
        _player.RagdollControl.SetActive(true);

        switch (GameManager.Instance.PlayerDeathType)
        {
            case DeathType.BY_EXPLOSION:
                SetExplotionEffect();
                break;
        }
    }

    public void SetExplotionEffect()
    {
        var center = (Transform) GameManager.Instance.PlayerDeathFelonObject;
        _player.RagdollControl.Explosion(center);
    }
}
