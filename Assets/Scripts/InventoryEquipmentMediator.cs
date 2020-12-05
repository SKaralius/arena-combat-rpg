using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class InventoryEquipmentMediator : MonoBehaviour
{
    GameObject Player;
    EquippedItems eqItems;
    #region Singleton logic
    public static InventoryEquipmentMediator instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        eqItems = Player.GetComponent<EquippedItems>();
    }

    public void Equip(EquippableItem item)
    {
        // If equip slot is empty
        if (eqItems.equipedItems[(int)item.Slot] == null)
        {
            eqItems.Equip(item);
            InventoryManager.instance.RemoveItemFromInventory(item, Player.GetHashCode());
        }
        // If there is an item already equiped
        else
        {
            EquippableItem previouslyEquippedItem;
            previouslyEquippedItem = eqItems.equipedItems[(int)item.Slot];
            // Sets the previously equipped item, to the slot in inventory.
            eqItems.Equip(item);
            InventoryManager.instance.inventory[InventoryManager.instance.inventory.IndexOf(item)] = previouslyEquippedItem;
            EventManager.ItemAddedToInventory();
        }
    }

    public void Unequip(EquippableItem item)
    {
        if (InventoryManager.instance.inventory.Count < 12)
        {
            eqItems.Unequip(item.Slot);
            InventoryManager.instance.AddItemToInventory(item, Player.GetHashCode());
        }
        else
        {
            MessageSystem.Print("Inventory is full");
        }
    }
}
