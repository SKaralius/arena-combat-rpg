using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [HideInInspector]
        public List<EquippableItem> inventory = new List<EquippableItem>();
        private GameObject player;
        private InventoryUIManager inventoryUIManager;
        private Gold gold;


        private void Awake()
        {
            inventoryUIManager = GetComponent<InventoryUIManager>();
            gold = GetComponentInChildren<Gold>();
            player = GameObject.FindGameObjectWithTag("Player");
            //AddItemToInventory(new EquippableItem(EquipSlot.Head, "Twice Helmet", ("Helmet", "First"), _sellPrice: 69, _damage: 5, _evasion: 30, _critical: 50, _skill: ESkills.HitTwice));
            //AddItemToInventory(new EquippableItem(EquipSlot.Legs, "First Jump Legs", ("Legs", "First"), _sellPrice: 69, _damage: 5, _evasion: 30, _skill: ESkills.Jump),
        }
        private void Start()
        {
            //AddItemToInventory(ItemGenerator.GenerateItem(1, skill: ESkills.Execute));
            //AddItemToInventory(new EquippableItem(EquipSlot.Legs, "Execute Legs", ("Pelvis", "First"), _sellPrice: 69, _skill: ESkills.Execute, _stats: new Unit.Stats()));
            gold.ChangeGold(0);
        }

        public void AddItemToInventory(EquippableItem item)
        {
            if (inventory.Count < 12)
            {
                inventory.Add(item);
                if (inventoryUIManager)
                    inventoryUIManager.UpdateUI();
                MessageSystem.Print($"{item.Name} was added to inventory");
            }
            else
            {
                MessageSystem.Print("Inventory is full.");
            }
        }

        public void RemoveItemFromInventory(EquippableItem item)
        {
            inventory.Remove(item);
            inventoryUIManager.UpdateUI();
            MessageSystem.Print($"{item.Name} was removed from inventory");
        }

        public void SellItem(EquippableItem item)
        {
            RemoveItemFromInventory(item);
            gold.ChangeGold(item.SellPrice);
        }
    }
}