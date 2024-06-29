using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomItemIcon : MonoBehaviour
{
    [SerializeField] private CustomItemSO m_itemSO;
    public CustomItemSO ItemSO
    {
        get => m_itemSO;
    }

    private bool IsUnlocked => CustomPlayerManager.Instance.IsItemUnlocked(m_itemSO.name);

    [SerializeField] private Image m_icon;
    [SerializeField] private CanvasGroup m_lockCanvas;
    [SerializeField] private EventTrigger m_eventTrigger;

    private void Awake()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerDownDelegate(); });
        m_eventTrigger.triggers.Add(entry);
    }

    

    public void SetIcon(CustomItemSO item, GenderType gender)
    {
        m_itemSO = item;
        m_icon.sprite = gender == GenderType.MALE ? m_itemSO.MaleIcon : m_itemSO.FemaleIcon;
        m_lockCanvas.alpha = IsUnlocked ? 0 : 1;
    }

    public void Refresh()
    {
        m_lockCanvas.alpha = IsUnlocked ? 0 : 1;
    }

    public void OnPointerDownDelegate()
    {
        if(IsUnlocked)
            CustomPlayerManager.Instance.AddItem(m_itemSO);
    }
}
