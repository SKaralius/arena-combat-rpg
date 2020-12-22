using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Shop : MonoBehaviour
    {
        [HideInInspector]
        public List<EquippableItem> inventory = new List<EquippableItem>();
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

        private void Start()
        {
            AddItemToShop(new EquippableItem(EquipSlot.RightWeapon, "Cool Sword", ("Weapon", "Sword"), 69, 50));
        }

        public void AddItemToShop(EquippableItem item)
        {
            if (inventory.Count < 12)
            {
                inventory.Add(item);
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
            if (gold.Wealth >= BuyPrice)
            {
                RemoveItemFromShop(item);
                gold.Wealth = -BuyPrice;
                inventoryManager.AddItemToInventory(item);
            }
            else
            {
                MessageSystem.Print("Not enough gold!");
            }
        }
    } 
}