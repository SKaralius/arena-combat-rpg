namespace Inventory
{
    public class EquipmentSlot : ItemSlot
    {
        private void Awake()
        {
            RenameButton("Unequip");
        }

        public sealed override void RenderUI(EquippableItem item)
        {
            if (item != null)
            {
                tooltipButton.gameObject.SetActive(true);

                itemName.text = item.Name;
                button.gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => { InventoryEquipmentMediator.instance.Unequip(item); });
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