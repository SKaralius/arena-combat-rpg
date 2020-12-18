﻿using Unit;
using UnityEngine;

namespace Inventory
{
    public class EquipmentSlot : ItemSlot
    {
        private PlayerEquippedItems equippedItems;
        private new void Awake()
        {
            base.Awake();
            equippedItems = GameObject.Find("Player").GetComponent<PlayerEquippedItems>();
            RenameButton("Unequip");
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            if (item != null)
            {
                PrepareTooltipButton();

                //itemName.text = item.Name;
                SetUpButton(() => equippedItems.Unequip(item.Slot));
            }
            else
            {
                SetSlotEmpty();
            }
        }
    } 
}