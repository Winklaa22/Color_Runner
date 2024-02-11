using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : View
{
    [Header("Popups")]
    [SerializeField] private View m_collectCoinsPopup;
    [SerializeField] private View m_choicePopup;

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        
        if(GameManager.Instance.Coins > 0)
            m_screenController.OpenPopup(m_collectCoinsPopup);
        else
            m_screenController.OpenPopup(m_choicePopup);
    }
}
