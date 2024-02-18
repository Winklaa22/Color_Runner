using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float m_duration = 3.0f;
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private CanvasGroup m_fadeBackground;
    
    private IEnumerator Start()
    {
        m_fadeBackground.DOFade(0, .5f);
        yield return new WaitForSeconds(m_duration);
        m_fadeBackground.DOFade(1, .5f);
        m_rectTransform.DOMoveY(-Screen.height, 0.5f).OnComplete(()=> { SceneManager.LoadScene("Menu"); });
    }
}
