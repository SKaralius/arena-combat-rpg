using UnityEngine;
using Unit;

namespace Inventory
{
    public class InventorySlot : ItemSlot
    {
        private GameObject shop;
        private InventoryManager inventoryManager;
        private EquippedItems equippedItems;

        private new void Awake()
        {
            base.Awake();
            equippedItems = GameObject.Find("Player").GetComponent<EquippedItems>();
            inventoryManager = GameObject.Find("InventoryPanel").GetComponent<InventoryManager>();
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            shop = GameObject.Find("ShopPanel");
            if (item != null)
            {
                //itemName.text = item.Name;
                SetUpTooltipButton(item);
                SetUpSprite(item);
            }
            else
            {
                SetSlotEmpty();
            }
            if (item is EquippableItem equipable)
            {
                SetUpButton(() => equippedItems.Equip(item));
            }
            if (shop != null && shop.activeSelf == true && item is EquippableItem)
            {
                RenameButton("Sell");
                SetUpButton(() => inventoryManager.SellItem(item));
            }
            else
            {
                RenameButton("Equip");
            }
        }
    }
}