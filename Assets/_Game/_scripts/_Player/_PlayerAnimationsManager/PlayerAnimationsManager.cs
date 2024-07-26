using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsManager : SceneSingleton<PlayerAnimationsManager>
{
    private PlayerAnimationsHandler _animationsHandler;
    public PlayerAnimationsHandler AnimationsHandler => _animationsHandler;

    public void SetAnimationHandler(Animator animator)
    {
        _animationsHandler = new PlayerAnimationsHandler(animator);
    }

    public void SetAction(AnimatorActionType actionType, string name, object value = null)
    {
        switch (actionType)
        {
            case AnimatorActionType.BOOL:
                _animationsHandler.SetBool(name, (bool) value);
                break;

            case AnimatorActionType.INTEGER:
                _animationsHandler.SetInt(name, (int) value);
                break;

            case AnimatorActionType.FLOAT:
                _animationsHandler.SetFloat(name, (float) value);
                break;

            case AnimatorActionType.TRIGGER:
                _animationsHandler.SetTrigger(name);
                break;
        }
    }
}
