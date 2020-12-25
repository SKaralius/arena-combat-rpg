using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class NPCEquippedItems : EquippedItems
    {
        private void Start()
        {
            Equip(ItemGenerator.GenerateItem(1, EquipSlot.RightWeapon));
            Equip(ItemGenerator.GenerateItem(1, EquipSlot.LeftWeapon));
            Equip(ItemGenerator.GenerateItem(1, EquipSlot.Legs));
            Equip(ItemGenerator.GenerateItem(1, EquipSlot.Chest));
        }
    }

}