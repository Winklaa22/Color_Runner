using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderTypeButton : NavigationButton
{
    [SerializeField] private GenderType m_genderType;
    [SerializeField] private CustomPlayerScreen m_screen;

    protected override void OnButtonClickedActions()
    {
        base.OnButtonClickedActions();

        ChangeGender();
    }

    public void ChangeGender()
    {
        m_screen.ChangeGender(m_genderType);
    }
}
