using UnityEngine;

namespace Inventory
{
    public class ShopSlot : ItemSlot
    {
        private Shop shop;
        private void Awake()
        {
            RenameButton("Buy");
            shop = GameObject.Find("ShopPanel").GetComponent<Shop>();
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            if (item != null)
            {
                tooltipButton.gameObject.SetActive(true);
                itemName.text = item.Name;
                button.gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => { shop.BuyItem(item); });
                RenderTooltip(item);
            }
            else
            {
                tooltipButton.gameObject.SetActive(false);
                itemName.text = "Empty";
                itemImage.sprite = null;
                button.gameObject.SetActive(false);
            }
        }
    } 
}