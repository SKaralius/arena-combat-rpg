using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlot : ItemSlot
    {
        private void Awake()
        {
            RenameButton("Equip");
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
        }
    }
}
