using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlot : ItemSlot
    {
        private GameObject player;
        private EquippedItems eqItems;
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            eqItems = player.GetComponent<EquippedItems>();
            RenameButton("Equip");
        }
        public sealed override void RenderUI()
        {
            if (_item != null)
            {
                itemName.text = _item.Name;
            }
            else
            {
                itemName.text = "Empty";
                itemImage.sprite = null;
                button.gameObject.SetActive(false);
            }
            if (_item is EquippableItem equipable)
            {
                    button.gameObject.SetActive(true);
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => { eqItems.Equip(equipable); });
            }
        }
    }
}
