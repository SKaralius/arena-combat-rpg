using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItems : MonoBehaviour
{
    public bool isPlayer;
    public IEquipable[] equipedItems = new IEquipable[5];
    public bool IsItemEquipped(IEquipable item)
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
    public void Equip(IEquipable item)
    {
        Debug.Log(equipedItems[(int)item.Slot]);
        if(equipedItems[(int)item.Slot] != null)
        {
            Unequip(item.Slot);
        }
        equipedItems[(int)item.Slot] = item;
        IItem i = (IItem)item;
        EventManager.ItemEquipped(i, gameObject.GetHashCode());
        MessageSystem.Print($"{i.Name} is equiped.");
    }
    public void Unequip(EquipSlot Slot)
    {
        IItem item = (IItem)equipedItems[(int)Slot];
        equipedItems[(int)Slot] = null;
        EventManager.ItemUnequipped(item, gameObject.GetHashCode());
        MessageSystem.Print($"{item.Name} is unequiped.");
    }
}
