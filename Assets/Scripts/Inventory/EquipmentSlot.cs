using Unit;
using UnityEngine;

namespace Inventory
{
    public class EquipmentSlot : ItemSlot
    {
        private PlayerEquippedItems equippedItems;
        private void Awake()
        {
            equippedItems = GameObject.Find("Player").GetComponent<PlayerEquippedItems>();
            RenameButton("Unequip");
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            if (item != null)
            {
                tooltipButton.gameObject.SetActive(true);

                //itemName.text = item.Name;
                button.gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => { equippedItems.Unequip(item.Slot); });
                RenderTooltip(item);
            }
            else
            {
                tooltipButton.gameObject.SetActive(false);

                //itemName.text = "Empty";
                //itemImage.sprite = null;
                button.gameObject.SetActive(false);
            }
        }
    } 
}