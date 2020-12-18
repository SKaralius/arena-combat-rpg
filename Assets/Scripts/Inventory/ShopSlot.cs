using UnityEngine;

namespace Inventory
{
    public class ShopSlot : ItemSlot
    {
        private Shop shop;
        private new void Awake()
        {
            base.Awake();
            shop = GameObject.Find("ShopPanel").GetComponent<Shop>();
            RenameButton("Buy");
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            if (item != null)
            {
                PrepareTooltipButton();
                //itemName.text = item.Name;

                SetUpButton(() => shop.BuyItem(item));
            }
            else
            {
                SetSlotEmpty();
            }
        }
    } 
}