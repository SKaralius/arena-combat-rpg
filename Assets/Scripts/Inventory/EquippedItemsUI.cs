using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class EquippedItemsUI : PanelUI
    {
        private EquippedItems eqItems;
        void Awake()
        {
            eqItems = GameObject.FindGameObjectWithTag("Player").GetComponent<EquippedItems>();
            EventManager.OnItemEquipped += UpdateUI;
            EventManager.OnItemUnequipped += UpdateUI;
            CreateSlot(5);
        }

        protected void UpdateUI(EquippableItem item, int who)
        {
            itemSlots[(int)item.Slot].RenderUI(eqItems.equipedItems[(int)item.Slot]);
        }
        private void OnDestroy()
        {
            EventManager.OnItemEquipped -= UpdateUI;
            EventManager.OnItemUnequipped -= UpdateUI;
        }
    }
}