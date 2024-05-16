using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderTypeButton : MonoBehaviour
{
    [SerializeField] private GenderType m_genderType;
    [SerializeField] private CustomPlayerScreen m_screen;

    public void ChangeGender()
    {
        CustomPlayerManager.Instance.ChangeGender(m_genderType);
        m_screen.OpenPanel(m_genderType);
    }
}
