using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TweenAnimationsController : MonoBehaviour
{
    [SerializeField] private TweenAnimator[] m_animators;

    
    public void AnimationsIn()
    {
        foreach(var animator in m_animators)
        {
            animator.AnimationIn();
        }
    }

    public void AnimationsOut()
    {
        foreach(var animator in m_animators)
        {
            animator.AnimationOut();
        }
    }

    public float GetMaximalDurationOfAnimations() => m_animators.OrderByDescending(obj => obj.Duration).First().Duration;

}
