using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathChoiseMenuPopup : View
{
    [SerializeField] private Button m_playAgainButton;
    [SerializeField] private Button m_returnToMenuButton;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_playAgainButton.onClick.AddListener(OnPlayAgainButton);
        m_returnToMenuButton.onClick.AddListener(OnReturnToMenuButton);
    }

    private void OnPlayAgainButton()
    {

    }

    private void OnReturnToMenuButton()
    {

    }
}
