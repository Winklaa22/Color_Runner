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
        var customManager = CustomPlayerManager.Instance;

        customManager.Entity_OnItemChanged += UpdateItem;
        customManager.Slots.ToList().ForEach(x => UpdateItem(x.ItemSO, customManager.Gender));
    }

    private void UpdateItem(CustomItemSO itemSO, GenderType gender)
    {
        if (gender == GenderType.FEMALE)
            m_beards.ToList().ForEach(x => x.SetItemActive(false));

        switch (itemSO.Type)
        {
            case CustomItemType.SKIN:
                m_skins.ToList().ForEach(x => x.SetItemActive(itemSO, gender));

                break;

            case CustomItemType.HAIR:
                m_hairs.ToList().ForEach(x => x.SetItemActive(itemSO, gender));

                break;

            case CustomItemType.HAT:
                m_hats.ToList().ForEach(x => x.SetItemActive(itemSO, gender));
                break;


            case CustomItemType.BEARD:
                m_beards.ToList().ForEach(x => x.SetItemActive(itemSO, gender));
                break;

            case CustomItemType.FACE_ACCESORIES:
                m_accesories.ToList().ForEach(x => x.SetItemActive(itemSO, gender));
                break;
        }
    }

}
