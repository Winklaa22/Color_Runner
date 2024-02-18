using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float m_duration = 3.0f;
    [SerializeField] private RectTransform m_rectTransform;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(m_duration);
        
        m_rectTransform.DOMoveY(-Screen.height, 0.3f).OnComplete(()=> { SceneManager.LoadScene("Menu"); });
    }
}
