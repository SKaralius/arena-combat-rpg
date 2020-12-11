﻿using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Shop : MonoBehaviour
    {
        [HideInInspector]
        public List<EquippableItem> inventory = new List<EquippableItem>();
        private InventoryManager inventoryManager;

        #region Singleton logic

        public static Shop instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton logic

        private void Start()
        {
            inventoryManager = GameObject.Find("InventoryPanel").GetComponent<InventoryManager>();
            AddItemToShop(new EquippableItem(EquipSlot.RightWeapon, "Cool Sword", ("Weapon", "Sword"), 69, 50));
        }

        public void AddItemToShop(EquippableItem item)
        {
            if (inventory.Count < 12)
            {
                inventory.Add(item);
                EventManager.ItemAddedToShop();
            }
            else
            {
                MessageSystem.Print("Inventory is full.");
            }
        }

        public void RemoveItemFromShop(EquippableItem item)
        {
            inventory.Remove(item);
            EventManager.ItemRemovedFromShop();
        }

        public void GenerateItems()
        {
            MessageSystem.Print("Items generated");
        }

        public void BuyItem(EquippableItem item)
        {
            if (inventoryManager.inventory.Count >= 12)
            {
                MessageSystem.Print("Not enough space in inventory.");
                return;
            }

            int BuyPrice = (int)item.SellPrice * 2;
            if (Gold.instance.Wealth >= BuyPrice)
            {
                RemoveItemFromShop(item);
                Gold.instance.Wealth = -BuyPrice;
                inventoryManager.AddItemToInventory(item, 0);
            }
            else
            {
                MessageSystem.Print("Not enough gold!");
            }
        }
    } 
}