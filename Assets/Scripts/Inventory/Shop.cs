using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Shop : MonoBehaviour
    {
        [HideInInspector]
        public List<EquippableItem> inventory = new List<EquippableItem>();
        private readonly int shopCapacity = 12;
        private InventoryManager inventoryManager;
        private Gold gold;

        private ShopUI shopUI;
        [SerializeField] GameObject inventoryPanel = null;

        private void Awake()
        {
            shopUI = GetComponent<ShopUI>();
            inventoryManager = inventoryPanel.GetComponent<InventoryManager>();
            gold = inventoryPanel.GetComponentInChildren<Gold>();
        }

        public void AddItemToShop(EquippableItem item)
        {
            if (inventory.Count < shopCapacity)
            {
                inventory.Add(item);
                if (shopUI)
                    shopUI.UpdateUI();
            }
            else
            {
                MessageSystem.Print("Inventory is full.");
            }
        }

        public void RemoveItemFromShop(EquippableItem item)
        {
            inventory.Remove(item);
            shopUI.UpdateUI();
        }

        public void GenerateItems()
        {
            inventory.Clear();
            for (int i = 0; i < shopCapacity; i++)
                AddItemToShop(ItemGenerator.GenerateItem(1));
        }

        public void BuyItem(EquippableItem item)
        {
            if (inventoryManager.inventory.Count >= 12)
            {
                MessageSystem.Print("Not enough space in inventory.");
                return;
            }

            int BuyPrice = (int)item.SellPrice * 5;
            if (gold.Wealth >= BuyPrice)
            {
                RemoveItemFromShop(item);
                gold.ChangeGold(-BuyPrice);
                inventoryManager.AddItemToInventory(item);
            }
            else
            {
                MessageSystem.Print("Not enough gold!");
            }
        }
    } 
}