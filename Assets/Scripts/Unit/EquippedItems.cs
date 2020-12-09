using UnityEngine;

namespace Unit
{
    public class EquippedItems : MonoBehaviour
    {
        public bool isPlayer;

        [System.NonSerialized]
        public EquippableItem[] equipedItems = new EquippableItem[5];

        private void Awake()
        {
            equipedItems[0] = null;
            equipedItems[1] = null;
            equipedItems[2] = null;
            equipedItems[3] = null;
            equipedItems[4] = null;
        }

        public bool IsItemEquipped(EquippableItem item)
        {
            foreach (IEquipable equipable in equipedItems)
            {
                if (equipable == null || item == null)
                {
                    continue;
                }
                if (equipable == item)
                {
                    return true;
                }
            }
            return false;
        }

        public void Equip(EquippableItem item)
        {
            //if(equipedItems[(int)item.Slot] != null)
            //{
            //    Unequip(item.Slot);
            //}
            equipedItems[(int)item.Slot] = item;
            EventManager.ItemEquipped(item, gameObject.GetHashCode());
            MessageSystem.Print($"{item.Name} is equiped.");
        }

        public void Unequip(EquipSlot Slot)
        {
            EquippableItem item = equipedItems[(int)Slot];
            equipedItems[(int)Slot] = null;
            EventManager.ItemUnequipped(item, gameObject.GetHashCode());
            MessageSystem.Print($"{item.Name} is unequiped.");
        }
    }
}