using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollSwipeHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public event Action PageChanged;

    [SerializeField] private Transform m_swipePanel;
    [SerializeField] private float m_space = 700f;
    [SerializeField] private float m_percentThreshold = .2f;
    [SerializeField] private float m_percent;
    [SerializeField] private int m_countOfPages = 3;
    [SerializeField] private int currentPage = 1;
    [SerializeField] private float m_sensivity = 1;

    [Header("Tween")]
    [SerializeField] private float m_tweenDuration = .3f;
    [SerializeField] private Ease m_tweenEase = Ease.OutExpo;
    [SerializeField] private float m_panelYLocation;

    public int CurrentPage => currentPage;

    private void Awake()
    {
        m_panelYLocation = m_swipePanel.localPosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var diff = eventData.pressPosition.y - eventData.position.y;
        m_swipePanel.localPosition = new Vector3(m_swipePanel.localPosition.x, m_panelYLocation - diff * m_sensivity, m_swipePanel.localPosition.z);
        m_percent = (eventData.pressPosition.y - eventData.position.y) / UnityEngine.Screen.height;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        var percentage = (eventData.pressPosition.y - eventData.position.y) / UnityEngine.Screen.height;

        
        if(Math.Abs(percentage) >= m_percentThreshold)
        {
            if(percentage < 0)
            {
                if(currentPage < m_countOfPages)
                {
                    currentPage++;
                    SwipeToNewLocation(m_space);
                }
                    
            }
            else
            {
                SwipeToPreviousLocation();
            }

            if(percentage > 0)
            {
                if(currentPage > 1)
                {
                    currentPage--;
                    SwipeToNewLocation(-m_space);
                }
            }
            else
            {
                SwipeToPreviousLocation();
            }
        }
        else
        {
            SwipeToPreviousLocation();
        }


    }

    private void SwipeToPreviousLocation()
    {
        m_swipePanel.DOLocalMoveY(m_panelYLocation, m_tweenDuration).SetEase(m_tweenEase);
    }

    private void SwipeToNewLocation(float yAxis)
    {
        var newLocation = m_panelYLocation + yAxis;
        m_swipePanel.DOLocalMoveY(newLocation, m_tweenDuration).SetEase(m_tweenEase);
        m_panelYLocation = newLocation;
    }

}
