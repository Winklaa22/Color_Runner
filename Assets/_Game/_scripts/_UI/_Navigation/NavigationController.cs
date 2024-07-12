using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NavigationController : MonoBehaviour
{
    [SerializeField] private NavigationButton[] m_buttons;
    [SerializeField] private bool m_isInteractable = true;
    public bool IsInteractable
    {
        get => m_isInteractable;
        set => m_isInteractable = value;
    }

    public void OnButtonClick(NavigationButton button)
    {
        m_buttons.First(btn => btn.IsActive && btn != button).SetActive(false);
    }
}
