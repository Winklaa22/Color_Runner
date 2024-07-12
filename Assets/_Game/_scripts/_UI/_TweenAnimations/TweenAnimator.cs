using CustomInspector;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAnimator : MonoBehaviour
{
    public event Action OnAnimationCompleted;

    [Header("Atributtes")]
    [SerializeField] private float m_dutation;
    public float Duration => m_dutation;
    [Header("Ease")]
    [SerializeField] private bool m_useEase;
    [SerializeField, ShowIf(nameof(m_useEase))] private Ease m_ease; 

    [Header("Fading")]
    [SerializeField] private bool m_useFade = false;
    [SerializeField, ShowIf(nameof(m_useFade)), Range(0, 1)] private float m_startFadeValue;
    [SerializeField, ShowIf(nameof(m_useFade)), Range(0, 1)] private float m_endFadeValue;
    [SerializeField, ShowIf(nameof(m_useFade))] private CanvasGroup m_canvasGroup;

    [Header("Movement")]
    [SerializeField] private bool m_useMovement;
    [SerializeField, ShowIf(BoolOperator.Or, nameof(m_useMovement), nameof(m_useDeltaSize))] private RectTransform m_rectTranform;
    [SerializeField, ShowIf(nameof(m_useMovement))] private Vector2 m_startMovePosition;
    [SerializeField, ShowIf(nameof(m_useMovement))] private Vector2 m_endMovePosition;

    [Header("DeltaSize")]
    [SerializeField] private bool m_useDeltaSize;
    [SerializeField, ShowIf(nameof(m_useDeltaSize))] private Vector2 m_startDeltasize;
    [SerializeField, ShowIf(nameof(m_useDeltaSize))] private Vector2 m_endDeltasize;

    public void AnimationIn()
    {
        if (m_useFade)
        {
            m_canvasGroup.alpha = m_startFadeValue;
            m_canvasGroup.DOFade(m_endFadeValue, m_dutation).SetEase(m_ease).SetUpdate(true).OnComplete(() => { OnAnimationCompleted?.Invoke(); });
        }

        if (m_useMovement)
        {
            m_rectTranform.anchoredPosition = m_startMovePosition;
            m_rectTranform.DOAnchorPos(m_endMovePosition, m_dutation).SetEase(m_ease).SetUpdate(true).OnComplete(() => { OnAnimationCompleted?.Invoke(); });
        }

        if (m_useDeltaSize)
        {
            m_rectTranform.sizeDelta = m_startDeltasize;
            m_rectTranform.DOSizeDelta(m_endDeltasize, m_dutation).SetEase(m_ease).SetUpdate(true).OnComplete(() => { OnAnimationCompleted?.Invoke(); });
        }
    }

    public void AnimationOut()
    {
        if (m_useFade)
        {
            m_canvasGroup.alpha = m_endFadeValue;

            m_canvasGroup.DOFade(m_startFadeValue, m_dutation).SetEase(m_ease).SetUpdate(true).OnComplete(() => { OnAnimationCompleted?.Invoke(); });
        }

        if (m_useMovement)
        {
            m_rectTranform.anchoredPosition = m_endMovePosition;
            m_rectTranform.DOAnchorPos(m_startMovePosition, m_dutation).SetEase(m_ease).SetUpdate(true).OnComplete(() => { OnAnimationCompleted?.Invoke(); });
        }

        if (m_useDeltaSize)
        {
            m_rectTranform.sizeDelta = m_endDeltasize;
            m_rectTranform.DOSizeDelta(m_startDeltasize, m_dutation).SetEase(m_ease).SetUpdate(true).OnComplete(() => { OnAnimationCompleted?.Invoke(); });
        }
    }
}
