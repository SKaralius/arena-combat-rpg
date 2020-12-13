using UnityEngine;
using Inventory;

namespace Unit
{
    public abstract class EquippedItems : MonoBehaviour
    {
        public EquippableItem[] EquippedItemsArray { get; protected set; } = new EquippableItem[5];
        protected ItemEquipGraphics itemEquipGraphics;
        protected CharacterSkills characterSkills;
        protected HealthBar healthBar;


        protected void Awake()
        {

            itemEquipGraphics = GetComponent<ItemEquipGraphics>();
            characterSkills = GetComponent<CharacterSkills>();
            healthBar = GetComponent<HealthBar>();
        }

        public virtual void Equip(EquippableItem item)
        {
            // If equip slot is empty
            if (EquippedItemsArray[(int)item.Slot] == null)
            {
                ForceEquip(item);
            }
            // If there is an item already equiped
            else
            {
                EquippableItem previouslyEquippedItem;
                previouslyEquippedItem = EquippedItemsArray[(int)item.Slot];
                EquippedItemsArray[(int)item.Slot] = null;
                // Sets the previously equipped item, to the slot in inventory.
                ForceEquip(item);
                characterSkills.RemoveFromSkillList(previouslyEquippedItem);
            }
        }

        public virtual void ForceEquip(EquippableItem item)
        {
            EquippedItemsArray[(int)item.Slot] = item;
            MessageSystem.Print($"{item.Name} is equiped on {gameObject.name}.");
            itemEquipGraphics.EquipItem(item);
            characterSkills.AddToSkillList(item);
            if (healthBar)
                healthBar.UpdateHealthBar(GetComponent<Controller>().Health);
        }
        //public virtual void Unequip(EquipSlot slot)
        //{
        //    Debug.LogWarning("NPC is unequipping items. NPC doesn't have inventory, what's the point of unequipping an item?");
        //}
    }
}