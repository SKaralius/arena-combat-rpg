using UnityEngine;
using TMPro;

namespace Inventory
{
    public class ShopSlot : ItemSlot
    {
        private Shop shop;
        [SerializeField] private TextMeshProUGUI price;

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
                SetUpTooltipButton(item);
                SetUpSprite(item);
                SetUpButton(() => shop.BuyItem(item));
                price.text = (item.SellPrice * 5).ToString();
            }
            else
            {
                SetSlotEmpty();
                price.text = "";
            }
        }
    } 
}