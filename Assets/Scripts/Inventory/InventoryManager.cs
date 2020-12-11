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


        private void Start()
        {
            inventoryUIManager = GetComponent<InventoryUIManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            //EventManager.OnItemEquipped += RemoveItemFromInventory;
            //EventManager.OnItemUnequipped += AddItemToInventory;
            //Gold.instance.Wealth = 10;
            //EventManager.GoldChanged(50);
            //AddItemToInventory(new EquippableItem(EquipSlot.RightWeapon, "Cool Sword", ("Weapon", "Sword"), _sellPrice: 50, _attackRange: 5, _damage:20), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.RightWeapon, "Staff", ("Weapon", "Staff"), _sellPrice: 69, _attackRange: 10, _damage: 5, _evasion: 5), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.Legs, "Evasion Legs", ("Pelvis", "First"), _sellPrice: 69, _evasion: 5, _skill: ESkills.BuffEvasion), player.GetHashCode());
            //AddItemToInventory(new EquippableItem(EquipSlot.Head, "Twice Helmet", ("Helmet", "First"), _sellPrice: 69, _damage: 5, _evasion: 30, _skill: ESkills.HitTwice), player.GetHashCode());
            //AddItemToInventory(new EquippableItem(EquipSlot.Legs, "First Jump Legs", ("Legs", "First"), _sellPrice: 69, _damage: 5, _evasion: 30, _skill: ESkills.Jump),
        }

        public void AddItemToInventory(EquippableItem item, int who)
        {
            if (who != player.GetHashCode())
                Debug.LogWarning("RemoveItemFromInventory called not by player");
            if (inventory.Count < 12)
            {
                inventory.Add(item);
                inventoryUIManager.UpdateUI();
                MessageSystem.Print($"{item.Name} was added to inventory");
            }
            else
            {
                MessageSystem.Print("Inventory is full.");
            }
        }

        public void RemoveItemFromInventory(EquippableItem item, int who)
        {
            if (who != player.GetHashCode())
                Debug.LogWarning("RemoveItemFromInventory called not by player");
            inventory.Remove(item);
            inventoryUIManager.UpdateUI();
            MessageSystem.Print($"{item.Name} was removed from inventory");
        }

        public void SellItem(EquippableItem item)
        {
            RemoveItemFromInventory(item, 0);
            Gold.instance.Wealth = (int)item.SellPrice;
        }
    }
}