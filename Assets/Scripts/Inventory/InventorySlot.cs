using UnityEngine;

namespace Inventory
{
    public class InventorySlot : ItemSlot
    {
        private GameObject shop;
        private InventoryManager inventoryManager;

        private void Awake()
        {
            inventoryManager = GameObject.Find("InventoryPanel").GetComponent<InventoryManager>();
            shop = GameObject.Find("ShopPanel");
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            if (item != null)
            {
                itemName.text = item.Name;
                tooltipButton.gameObject.SetActive(true);
                RenderTooltip(item);
            }
            else
            {
                tooltipButton.gameObject.SetActive(false);
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
                button.onClick.AddListener(() => { inventoryManager.SellItem(item); });
            }
            else
            {
                RenameButton("Equip");
            }
        }
    }
}