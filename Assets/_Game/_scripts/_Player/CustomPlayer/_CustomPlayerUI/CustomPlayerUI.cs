using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomPlayerUI : MonoBehaviour
{
    [System.Serializable]
    private class Category
    {
        public CustomItemType Type;
        public Button NavigationButton;
        public CustomPlayerCategoryController Controller;
        public float XPosition;
    }

    [SerializeField] private Category[] m_categories;
    [SerializeField] private RectTransform m_rectTransform;

    private void Awake()
    {
        foreach(var category in m_categories)
        {
            category.NavigationButton.onClick.AddListener(delegate { MoveToCategory(category.XPosition); });
        }
    }

    private void MoveToCategory(float position)
    {
        m_rectTransform.DOLocalMoveX(position, 0.3f);
    }

    public void RefreshIconsInEachCategory()
    {
        m_categories.ToList().ForEach(x => x.Controller.RefreshAllIcons());
    }
}

