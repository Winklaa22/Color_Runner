using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayerUI : MonoBehaviour
{
    [System.Serializable]
    private class Category
    {
        public string Name;
        public CustomItemType Type;
        public CustomPlayerCategoryController Controller;
        public float XPosition;
    }

    [Header("Categories")]
    [SerializeField] private TMP_Text m_categoryNameText; 
    [SerializeField] private Category[] m_categories;
    [SerializeField] private RectTransform m_rectTransform;
    private int _currentCategory = 0;

    [Header("Arrows")]
    [SerializeField] private Button m_leftArrow;
    private TweenAnimator _leftArrowAnimator;
    [SerializeField] private Button m_rightArrow;
    private TweenAnimator _rightArrowAnimator;

    private void Awake()
    {
        m_leftArrow.onClick.AddListener(MoveToPreviousCategory);
        m_rightArrow.onClick.AddListener(MoveToNextCategory);
    }

    private void MoveToNextCategory()
    {
        
        _currentCategory = _currentCategory < m_categories.Length - 1 ? _currentCategory + 1 : 0;
        var position = m_categories[_currentCategory].XPosition;
        m_rectTransform.DOLocalMoveX(position, 0.3f);
        RefreshCategoryName();

    }

    private void MoveToPreviousCategory()
    {
        _currentCategory = _currentCategory > 0 ? _currentCategory - 1 : m_categories.Length - 1;
        var position = m_categories[_currentCategory].XPosition;
        m_rectTransform.DOLocalMoveX(position, 0.3f);
        RefreshCategoryName();
    }

    private void RefreshCategoryName()
    {
        m_categoryNameText.text = m_categories[_currentCategory].Name;
    }

    public void RefreshIconsInEachCategory()
    {
        m_categories.ToList().ForEach(x => x.Controller.RefreshAllIcons());
    }
}

