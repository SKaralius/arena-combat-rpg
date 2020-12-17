using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class NPCEquippedItems : EquippedItems
    {
        private void Start()
        {
            Equip(new EquippableItem(EquipSlot.Chest, "Health Chest", ("Chest", "First"), _sellPrice: 69, _health: 500));
        }
    }

}