using CustomInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class View : MonoBehaviour, IView
{
    [Header("UI")]
    [SerializeField] private GameObject m_view;

    [Header("Animations")]
    [SerializeField] private bool m_useAnimations;
    [SerializeField] private List<TweenAnimator> m_animators;

    //Events
    public delegate void OnOpen();
    public event Action Entity_OnOpen;

    public delegate void OnClose();
    public event Action Entity_OnClosed;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }

    public void Open()
    {
        m_view.SetActive(true);
        if (m_useAnimations)
        {
            foreach (var animator in m_animators)
            {
                animator.AnimationIn();
            }
        }
    }


    public void Close()
    {
        if (m_useAnimations)
        {
            StartCoroutine(CloseWithAnimations());
        }
        else
            m_view.SetActive(false);
    }

    private IEnumerator CloseWithAnimations()
    {
        var duration = m_animators.OrderByDescending(obj => obj.Duration).First().Duration;
        foreach (var animator in m_animators)
        {
            animator.AnimationOut();
        }
        yield return new WaitForSeconds(duration);
        m_view.SetActive(false);

    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }
}
