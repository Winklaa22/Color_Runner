using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawSkinScreen : View
{
    [Header("Drawing panel")]
    [SerializeField] private TMP_Text m_packName;
    [SerializeField] private Image m_icon;
    [SerializeField] private Transform m_iconTranform;
    [SerializeField] private TweenAnimator m_questionMarkIcon;
    [SerializeField] private DrawController m_drawController;
    [SerializeField] private Button m_drawButton;


    [Header("After draw")]
    [SerializeField] private TMP_Text m_drawnText;
    [SerializeField] private TweenAnimator m_drawnTextAnimator;
    [SerializeField] private string[] m_drawnTextVariances;
    [SerializeField] private TweenAnimator m_tryAgainButton;
    [SerializeField] private TweenAnimator m_goToMenuButton;
    
    private CanvasGroup _drawButtonCanvas;
    private CanvasGroup _iconCanvas;
    private TweenAnimator m_packNameAnimator;

    protected override void OnAwake()
    {
        base.OnAwake();
        _iconCanvas = m_icon.GetComponent<CanvasGroup>();
        _drawButtonCanvas = m_drawButton.GetComponent<CanvasGroup>();
        m_packNameAnimator = m_packName.GetComponent<TweenAnimator>();
        
        m_drawButton.onClick.AddListener(Draw);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        _drawButtonCanvas.DOKill();
        m_drawButton.interactable = true;
        _drawButtonCanvas.alpha = 1;
        m_packName.text = DrawManager.Instance.GetCurrentPack().Name;;

    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        m_iconTranform.DOScale(1f, .5f).SetEase(Ease.InOutExpo);
        _iconCanvas.DOFade(0, .3f).SetEase(Ease.InOutExpo);
        m_questionMarkIcon.AnimationIn();
    }

    private void Draw()
    {
        StopAllCoroutines();
        m_drawButton.interactable = false;
        _drawButtonCanvas.DOFade(0, 0.3f).SetEase(Ease.InOutExpo);
        _iconCanvas.DOKill();
        _iconCanvas.alpha = 0;

        StartCoroutine(DrawingProcess());
    }

    public void TryAgain()
    {
        m_tryAgainButton.AnimationOut();
        m_goToMenuButton.AnimationOut();
        m_packNameAnimator.AnimationIn();
        m_iconTranform.DOScale(1f, .5f).SetEase(Ease.InOutExpo);
        _iconCanvas.DOFade(0, .3f).SetEase(Ease.InOutExpo);
        m_questionMarkIcon.AnimationIn();
        _drawButtonCanvas.DOFade(1, 0.3f).SetEase(Ease.InOutExpo);
        m_drawButton.interactable = true;
    }

    public void BackToMenu()
    {
        ScreensManager.Instance.OpenScreen(ScreenType.MAIN_MENU);
    }


    private IEnumerator DrawingProcess()
    {

        m_questionMarkIcon.AnimationOut();
        yield return new WaitForSeconds(m_questionMarkIcon.Duration);
        _iconCanvas.DOFade(1f, 4.0f);

        var pack = DrawManager.Instance.GetCurrentPack();
        var time = 0.05f;
        CustomItemSO randomSkin = null;

        for (int i = 0; i <= 100; i++)
        {
            randomSkin = pack.Items[Random.Range(0, pack.Items.Length - 1)];
            m_icon.sprite = randomSkin.GetIconToDraw();
            yield return new WaitForSeconds(time);

            if (i > 80)
                time += 0.01f;
        }

        yield return new WaitForSeconds(1.5f);
        m_packNameAnimator.AnimationOut();
        m_iconTranform.DOScale(1.5f, .5f).SetEase(Ease.InOutExpo);
        m_drawnText.text = CustomPlayerManager.Instance.IsItemUnlocked(randomSkin.name) ? m_drawnTextVariances[0] : m_drawnTextVariances[1];
        CustomPlayerManager.Instance.SetItemUnlocked(randomSkin);
        m_drawnTextAnimator.AnimationIn();
        
        yield return new WaitForSeconds(2f);
        m_drawnTextAnimator.AnimationOut();
        yield return new WaitForSeconds(.2f);
        m_tryAgainButton.AnimationIn();
        m_goToMenuButton.AnimationIn();
    }
}
