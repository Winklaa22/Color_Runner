using System.Collections;
using DG.Tweening;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : SceneSingleton<SceneTransitionManager>
{
    [SerializeField] private CanvasGroup m_fadeBackground;
    [SerializeField] private float m_tweenDuration = .3f;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void OnStart()
    {
        base.OnStart();
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(FadeOut());
    }
    public void LoadScene(string sceneName)
    {
        m_fadeBackground.DOFade(1, m_tweenDuration).OnComplete(()=>
        {
            SceneManager.LoadScene(sceneName);
        });
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.1f);
        m_fadeBackground.DOFade(0, m_tweenDuration);
    }
    
}
