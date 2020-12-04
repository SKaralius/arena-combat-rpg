using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : ItemSlot
{
    private EquippedItems eqItems;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        eqItems = player.GetComponent<EquippedItems>();
        RenameButton("Unequip");
    }
    public  sealed override void RenderUI()
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
        button.gameObject.SetActive(true);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { eqItems.Unequip(_item.Slot); });
    }
}
