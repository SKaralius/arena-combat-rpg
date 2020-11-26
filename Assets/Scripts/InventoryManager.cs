using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class InventoryManager : MonoBehaviour
{
    public List<IItem> inventory = new List<IItem>();
    
    public static InventoryManager instance;
    #region Singleton logic
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
        AddItemToInventory(new RightWeapon("Cool Sword", 69, 50));
        AddItemToInventory(new LeftWeapon("Lame Sword", 69, 5));
        AddItemToInventory(new Chest("Cool Chest", 69, _health: 20, _damage: 17));
        AddItemToInventory(new Legs("Cool Legs", 69, _health: 20, _damage: 40));
        AddItemToInventory(new Head("Cool Head", 69, _health: 20, _damage: 40));
    }
    public void AddItemToInventory(IItem item)
    {
        if (inventory.Count < 12)
        {
            inventory.Add(item);
            EventManager.ItemAddedToInventory();
            MessageSystem.Print($"{item.Name} was added to inventory");
        } else
        {
            MessageSystem.Print("Inventory is full.");
        }

    }
    public void RemoveItemFromInventory(IItem item)
    {
        inventory.Remove(item);
        EventManager.ItemRemovedFromInventory();
        MessageSystem.Print($"{item.Name} was removed from inventory");
    }
}
