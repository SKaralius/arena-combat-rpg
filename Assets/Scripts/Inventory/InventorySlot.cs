using UnityEngine;
using Unit;

namespace Inventory
{
    public class InventorySlot : ItemSlot
    {
        private GameObject shop;
        private InventoryManager inventoryManager;
        private EquippedItems equippedItems;

        private void Awake()
        {
            equippedItems = GameObject.Find("Player").GetComponent<EquippedItems>();
            inventoryManager = GameObject.Find("InventoryPanel").GetComponent<InventoryManager>();
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            shop = GameObject.Find("ShopPanel");
            if (item != null)
            {
                //itemName.text = item.Name;
                tooltipButton.gameObject.SetActive(true);
                RenderTooltip(item);
            }
            else
            {
                tooltipButton.gameObject.SetActive(false);
                //itemName.text = "Empty";
                //itemImage.sprite = null;
                button.gameObject.SetActive(false);
            }
            if (item is EquippableItem equipable)
            {
                button.gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => { equippedItems.Equip(item); });
            }
            if (shop != null && shop.activeSelf == true)
            {
                RenameButton("Sell");
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => { inventoryManager.SellItem(item); });
            }
            else
            {
                RenameButton("Equip");
            }
        }
    }
}