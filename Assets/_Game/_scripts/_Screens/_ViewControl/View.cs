using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public abstract class View : MonoBehaviour, IView
{
    [Header("UI")]
    [SerializeField] private GameObject m_view;

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
    }

    public void Close()
    {
        m_view.SetActive(false);
    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }


}
