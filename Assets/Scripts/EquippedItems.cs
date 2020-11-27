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
        if(equipedItems[(int)item.Slot] != null)
        {
            Unequip(item.Slot);
        }
        equipedItems[(int)item.Slot] = item;
        IItem i = (IItem)item;
        Debug.Log(isPlayer);
        if (isPlayer)
            EventManager.ItemEquipped(i);
        MessageSystem.Print($"{i.Name} is equiped.");
    }
    public void Unequip(EquipSlot Slot)
    {
        IItem item = (IItem)equipedItems[(int)Slot];
        equipedItems[(int)Slot] = null;
        if (isPlayer)
            EventManager.ItemUnequipped(item);
        MessageSystem.Print($"{item.Name} is unequiped.");
    }
}
