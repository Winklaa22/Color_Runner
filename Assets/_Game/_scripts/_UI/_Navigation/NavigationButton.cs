using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public abstract class NavigationButton : MonoBehaviour
{
    [SerializeField] private TweenAnimator m_animator;
    [SerializeField] private NavigationController m_navController;
    [SerializeField] private bool m_isDefault;
    private EventTrigger _eventSystem;
    protected bool _isActive = false;
    public bool IsActive => _isActive;
    public delegate void OnClicked();
    public event OnClicked Entity_OnClicked;

    private void Awake()
    {
        _eventSystem = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => 
        {
            OnButtonClicked();
        });

        _eventSystem.triggers.Add(entry);   
    }

    

    private void OnButtonClicked()
    {
        if (_isActive || !m_navController.IsInteractable)
            return;

        SetActive(true);
        OnButtonClickedActions();
        m_navController.OnButtonClick(this);
        Entity_OnClicked?.Invoke();
    }

    protected virtual void OnButtonClickedActions() { }


    public void SetActive(bool active)
    {
        _isActive = active;

        if (_isActive)
            m_animator.AnimationIn();
        else
            m_animator.AnimationOut();
    }
}
