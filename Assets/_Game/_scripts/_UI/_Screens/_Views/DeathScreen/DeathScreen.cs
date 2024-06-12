using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : View
{
    [Header("Popups")]
    [SerializeField] private View m_collectCoinsPopup;
    [SerializeField] private View m_choicePopup;
    [SerializeField] private TMP_Text m_metersCounter;

    protected override void OnViewOpened()
    {
        base.OnViewOpened();

        m_metersCounter.text = m_metersCounter.text.Replace("%", ((int) GameManager.Instance.Meters).ToString());
        
        if(GameManager.Instance.Coins > 0)
            m_screenController.OpenPopup(m_collectCoinsPopup);
        else
            m_screenController.OpenPopup(m_choicePopup);
    }
}
