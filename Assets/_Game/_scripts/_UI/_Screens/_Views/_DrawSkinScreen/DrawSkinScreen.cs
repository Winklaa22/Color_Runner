using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawSkinScreen : View
{
    [Header("Draw system")]
    [SerializeField] private int m_drawingTimes = 100;
    [SerializeField] private float m_percentToSlowdown = 80.0f;

    [Header("Sequence delays")]
    [SerializeField] private float m_startDrawDelay = .05f;
    [SerializeField] private float m_slowingdownDrawDelay = .01f;
    [SerializeField] private float m_timeToShowDraw = 1.5f;
    [SerializeField] private float m_drawnTextDuration = 2.0f;
    [SerializeField] private float m_delayToShowButtons = .2f;

    [Header("Icon Fade animations")]
    [SerializeField] private float m_iconFadeInDuration = 4.0f;
    [SerializeField] private float m_iconFadeOutDuration = .5f;

    [Header("Icon Scale animation")]
    [SerializeField] private float m_iconScaleUpDownDuration = .3f;
    [SerializeField] private float m_iconScaleUpValue = 1.5f;
    [SerializeField] private float m_iconPrimaryScaleValue = 1.0f;

    [Header("Drawing panel")]
    [SerializeField] private TMP_Text m_packName;
    [SerializeField] private Image m_icon;
    [SerializeField] private TweenAnimator m_questionMarkIcon;
    [SerializeField] private Button m_drawButton;


    [Header("After draw")]
    [SerializeField] private TMP_Text m_drawnText;
    [SerializeField] private TweenAnimator m_drawnTextAnimator;
    [TextArea(5, 5)]
    [SerializeField] private string[] m_drawnTextVariances;
    [SerializeField] private Button m_tryAgainButton;
    [SerializeField] private TweenAnimator m_tryAgainButtonAnimator;
    [SerializeField] private Button m_goToMenuButton;
    [SerializeField] private TweenAnimator m_goToMenuButtonAnimator;
    private CanvasGroup _drawButtonCanvas;
    private CanvasGroup _iconCanvas;

    protected override void OnAwake()
    {
        base.OnAwake();
        _iconCanvas = m_icon.GetComponent<CanvasGroup>();
        _drawButtonCanvas = m_drawButton.GetComponent<CanvasGroup>();
        
        m_drawButton.onClick.AddListener(Draw);
        m_tryAgainButton.onClick.AddListener(OnTryAgainButton);
        m_goToMenuButton.onClick.AddListener(BackToMenu);
    }

    protected override void OnViewOpened()
    {
        base.OnViewOpened();
        _drawButtonCanvas.DOKill();
        m_drawButton.interactable = true;
        _drawButtonCanvas.alpha = 1;
        m_packName.text = DrawManager.Instance.GetCurrentPack().Name;
        ShopManager.Instance.Entity_OnVirtualProductHasBought += OnProductHasBought;

    }

    protected override void OnViewClosed()
    {
        base.OnViewClosed();
        //m_iconTranform.DOScale(1f, .5f).SetEase(Ease.InOutExpo);
        SetIconFadeOut();
        MainMenuManager.Instance.SetCoinsCounterActive(false);
        m_questionMarkIcon.AnimationIn();
        ShopManager.Instance.Entity_OnVirtualProductHasBought -= OnProductHasBought;
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

    public void OnTryAgainButton()
    {
        ShopManager.Instance.BuyProduct(ShopManager.Instance.GetSkinPackProduct(DrawManager.Instance.SkinPackType));
    }

    private void OnProductHasBought(ProductSO product)
    {
        if(product.Type == ShopManager.Instance.GetSkinPackProduct(DrawManager.Instance.SkinPackType))
        {
            TryAgain();
        }
    }

    private void TryAgain()
    {
        m_tryAgainButtonAnimator.AnimationOut();
        m_goToMenuButtonAnimator.AnimationOut();
        SetIconFadeOut();
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
        _iconCanvas.DOFade(1.0f, m_iconFadeInDuration);

        var pack = DrawManager.Instance.GetCurrentPack();
        var time = m_startDrawDelay;
        CustomItemSO randomSkin = null;

        for (int i = 0; i <= m_drawingTimes; i++)
        {
            randomSkin = pack.Items[Random.Range(0, pack.Items.Length - 1)];
            m_icon.sprite = randomSkin.GetIconToDraw();
            yield return new WaitForSeconds(time);

            if (i > (m_drawingTimes * (m_percentToSlowdown / 100)))
                time += m_slowingdownDrawDelay;
        }

        yield return new WaitForSeconds(m_timeToShowDraw);
        m_drawnText.text = CustomPlayerManager.Instance.IsItemUnlocked(randomSkin.name) ? m_drawnTextVariances[0] : m_drawnTextVariances [1];
        CustomPlayerManager.Instance.SetItemUnlocked(randomSkin);
        m_drawnTextAnimator.AnimationIn();
        yield return new WaitForSeconds(m_drawnTextDuration); 
        m_drawnTextAnimator.AnimationOut();
        yield return new WaitForSeconds(m_delayToShowButtons);
        m_tryAgainButtonAnimator.AnimationIn();
        m_goToMenuButtonAnimator.AnimationIn();
    }

    private void SetIconFadeOut()
    {
        _iconCanvas.DOFade(0, m_iconFadeOutDuration).SetEase(Ease.InOutExpo);
    }
}
