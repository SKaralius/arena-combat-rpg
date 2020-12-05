using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : ItemSlot
{
    private void Awake()
    {
        RenameButton("Unequip");
    }
    public  sealed override void RenderUI(EquippableItem item)
    {
        if (item != null)
        {
            itemName.text = item.Name;
            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { InventoryEquipmentMediator.instance.Unequip(item); });
        }
        else
        {
            itemName.text = "Empty";
            itemImage.sprite = null;
            button.gameObject.SetActive(false);
        }
    }
}
