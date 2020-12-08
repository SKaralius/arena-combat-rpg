using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [HideInInspector]
        public List<EquippableItem> inventory = new List<EquippableItem>();
        private GameObject player;

        #region Singleton logic
        public static InventoryManager instance;
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
            player = GameObject.FindGameObjectWithTag("Player");
            //EventManager.OnItemEquipped += RemoveItemFromInventory;
            //EventManager.OnItemUnequipped += AddItemToInventory;
            Gold.instance.Wealth = 10;
            EventManager.GoldChanged(50);
            //AddItemToInventory(new EquippableItem(EquipSlot.RightWeapon, "Cool Sword", ("Weapon", "Sword"), 69, 50), player.GetHashCode());
            //AddItemToInventory(new EquippableItem(EquipSlot.RightWeapon, "Lame Staff", ("Weapon", "Staff"), 69, 5), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.Head, "Twice Helmet", ("Helmet", "First"), _sellPrice: 69, _damage: 5, _evasion: 30, _skill: Skills.ESkills.HitTwice), player.GetHashCode());
            //AddItemToInventory(new EquippableItem(EquipSlot.Legs, "First Jump Legs", ("Legs", "First"), _sellPrice: 69, _damage: 5, _evasion: 30, _skill: Skills.ESkills.Jump), player.GetHashCode());
            //AddItemToInventory(new EquippableItem(EquipSlot.Chest, "First Chest", ("Chest", "First"), _sellPrice: 69, _damage: 5, _evasion: 30), player.GetHashCode());
            //AddItemToInventory(new EquippableItem(EquipSlot.Legs, "First Legs", ("Pelvis", "First"), _sellPrice: 69, _damage: 5, _evasion: 30), player.GetHashCode());
            //AddItemToInventory(new EquippableItem(EquipSlot.LeftWeapon, "Left Sword", ("Weapon", "Sword"), _sellPrice: 69, _damage: 5, _evasion: 30), player.GetHashCode());
        }
        public void AddItemToInventory(EquippableItem item, int who)
        {
            if (who != player.GetHashCode())
                Debug.LogWarning("RemoveItemFromInventory called not by player");
            if (inventory.Count < 12)
            {
                inventory.Add(item);
                EventManager.ItemAddedToInventory();
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
            EventManager.ItemRemovedFromInventory();
            MessageSystem.Print($"{item.Name} was removed from inventory");
        }
        public void SellItem(EquippableItem item)
        {
            RemoveItemFromInventory(item, 0);
            Gold.instance.Wealth = (int)item.SellPrice;
        }
    }
}
