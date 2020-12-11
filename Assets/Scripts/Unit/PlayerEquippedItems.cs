using Inventory;
using UnityEngine;

namespace Unit
{
    public class PlayerEquippedItems : EquippedItems
    {
        private EquippedItemsUI equippedItemsUI;
        new private void Start()
        {
            base.Start();
            equippedItemsUI = GameObject.Find("Equipment").GetComponent<EquippedItemsUI>();
        }

        public override void Equip(EquippableItem item)
        {
            base.Equip(item);
            // Call UI
            SkillManager skillManager = GetComponent<SkillManager>();
            if (skillManager.enabled == true)
                skillManager.RenderSkillSlots();
            equippedItemsUI.UpdateUI(item.Slot);
        }

        public override void Unequip(EquipSlot Slot)
        {
            base.Unequip(Slot);
            // Call UI
            SkillManager skillManager = GetComponent<SkillManager>();
            if (skillManager.enabled == true)
                skillManager.RenderSkillSlots();
            equippedItemsUI.UpdateUI(Slot);
        }
    }
}