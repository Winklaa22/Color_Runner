using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    [SerializeField] private CustomItemController[] m_skins;
    [SerializeField] private CustomItemController[] m_hairs;
    [SerializeField] private CustomItemController[] m_beards;
    [SerializeField] private CustomItemController[] m_hats;
    [SerializeField] private CustomItemController[] m_accesories;

    [System.Serializable]
    private class CustomizationSlot
    {
        public CustomItemType Type;
    }

    private void Start()
    {
        CustomPlayerManager.Instance.Entity_OnItemChanged += UpdateItem;
    }

    private void UpdateItem(CustomItemSO itemSO)
    {
        switch (itemSO.Type)
        {
            case CustomItemType.SKIN:
                foreach (var skin in m_skins)
                {
                    skin.ItemObject.SetActive(skin.ItemSO == itemSO);
                }
                break;

            case CustomItemType.HAIR:
                foreach (var hair in m_hairs)
                {
                    hair.ItemObject.SetActive(hair.ItemSO == itemSO);
                }
                break;

            case CustomItemType.HAT:
                foreach (var hat in m_hats)
                {
                    hat.ItemObject.SetActive(hat.ItemSO == itemSO);
                }
                break;


            case CustomItemType.BEARD:
                foreach (var beard in m_beards)
                {
                    beard.ItemObject.SetActive(beard.ItemSO == itemSO);
                }
                break;

            case CustomItemType.FACE_ACCESORIES:
                foreach (var acc in m_accesories)
                {
                    acc.ItemObject.SetActive(acc.ItemSO == itemSO);
                }
                break;
        }
    }

}
