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
}
