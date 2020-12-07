﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlot : ItemSlot
    {
        GameObject shop;
        private void Awake()
        {
            shop = GameObject.Find("ShopPanel");
        }
        public sealed override void RenderUI(EquippableItem item)
        {
            if (item != null)
            {
                itemName.text = item.Name;
            }
            else
            {
                itemName.text = "Empty";
                itemImage.sprite = null;
                button.gameObject.SetActive(false);
            }
            if (item is EquippableItem equipable)
            {
                    button.gameObject.SetActive(true);
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => { InventoryEquipmentMediator.instance.Equip(item); });
            }
            if (shop.activeSelf == true)
            {
                RenameButton("Sell");
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => { InventoryManager.instance.SellItem(item); });
            } 
            else
            {
                RenameButton("Equip");
            }
        }
    }
}
