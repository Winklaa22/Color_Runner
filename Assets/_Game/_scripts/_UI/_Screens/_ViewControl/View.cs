using CustomInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class View : MonoBehaviour, IView
{
    [Header("Controller of all views")]
    [SerializeField] protected ScreenController m_screenController;

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

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {

    }

    public void Open()
    {
        OnViewOpened();
        m_view.SetActive(true);
        if (m_useAnimations)
        {
            foreach (var animator in m_animators)
            {
                animator.AnimationIn();
            }
        }
    }

    protected virtual void OnViewOpened()
    {

    }


    public void Close()
    {
        OnViewClosed();

        if (m_useAnimations)
        {
            StartCoroutine(CloseWithAnimations());
        }
        else
            m_view.SetActive(false);
    }

    protected virtual void OnViewClosed()
    {

    }

    private IEnumerator CloseWithAnimations()
    {
        var duration = GetMaximalDurationOfAnimations();
        foreach (var animator in m_animators)
        {
            animator.AnimationOut();
        }
        yield return new WaitForSecondsRealtime(duration);
        m_view.SetActive(false);

    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }

    protected float GetMaximalDurationOfAnimations() => m_animators.OrderByDescending(obj => obj.Duration).First().Duration;
}
