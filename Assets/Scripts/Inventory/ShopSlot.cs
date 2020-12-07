using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : ItemSlot
{
    private void Awake()
    {
        RenameButton("Buy");
    }
    public sealed override void RenderUI(EquippableItem item)
    {
        if (item != null)
        {
            itemName.text = item.Name;
            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { Shop.instance.BuyItem(item); });
        }
        else
        {
            itemName.text = "Empty";
            itemImage.sprite = null;
            button.gameObject.SetActive(false);
        }
    }
}
