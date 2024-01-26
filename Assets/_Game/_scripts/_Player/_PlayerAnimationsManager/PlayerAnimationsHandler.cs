using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsHandler
{
    private Animator _animator;

    public PlayerAnimationsHandler(Animator anim)
    {
        _animator = anim;
    }

    public void SetFloat(string name, float value)
    {
        _animator.SetFloat(name, value);
    }

    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
    }

    public void SetInt(string name, int value)
    {
        _animator.SetInteger(name, value);
    }

    public void SetBool(string name, bool condition)
    {
        _animator.SetBool(name, condition);
    }
}
