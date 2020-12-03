﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public List<IItem> inventory = new List<IItem>();
        private GameObject player;

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
            player = GameObject.FindGameObjectWithTag("Player");
            EventManager.OnItemEquipped += RemoveItemFromInventory;
            EventManager.OnItemUnequipped += AddItemToInventory;
            AddItemToInventory(new EquippableItem(EquipSlot.RightWeapon, "Cool Sword", ("Weapon", "Sword"), 69, 50), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.RightWeapon, "Lame Staff", ("Weapon", "Staff"), 69, 5), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.Head, "First Helmet", ("Helmet", "First"), _sellPrice: 69, _damage: 5, _evasion: 30), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.Chest, "First Chest", ("Chest", "First"), _sellPrice: 69, _damage: 5, _evasion: 30), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.Legs, "First Legs", ("Pelvis", "First"), _sellPrice: 69, _damage: 5, _evasion: 30), player.GetHashCode());
            AddItemToInventory(new EquippableItem(EquipSlot.LeftWeapon, "Left Sword", ("Weapon", "Sword"), _sellPrice: 69, _damage: 5, _evasion: 30), player.GetHashCode());
        }
        public void AddItemToInventory(IItem item, int who)
        {
            if (who != player.GetHashCode())
                return;
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
        public void RemoveItemFromInventory(IItem item, int who)
        {
            if (who != player.GetHashCode())
                return;
            inventory.Remove(item);
            EventManager.ItemRemovedFromInventory();
            MessageSystem.Print($"{item.Name} was removed from inventory");
        }
    }
}