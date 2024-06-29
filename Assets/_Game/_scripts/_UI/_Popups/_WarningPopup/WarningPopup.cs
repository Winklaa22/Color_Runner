using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopup : MonoBehaviour
{
    [SerializeField] private TweenAnimationsController m_tweenController;
    [SerializeField] private Image m_background;
    [SerializeField] private float m_duration;
    [SerializeField] private TMP_Text m_warningText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ShowPopup(string text)
    {
        m_warningText.text = text;
        StartCoroutine(WarningProcess());
    }

    private IEnumerator WarningProcess()
    {
        m_tweenController.AnimationsIn();
        m_background.raycastTarget = true;
       yield return new WaitForSeconds(m_tweenController.GetMaximalDurationOfAnimations() + m_duration);
        m_background.raycastTarget = false;
        m_tweenController.AnimationsOut();
    }


}
