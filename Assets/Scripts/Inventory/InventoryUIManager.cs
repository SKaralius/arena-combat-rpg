namespace Inventory
{
    public class InventoryUIManager : PanelUI
    {
        private void Awake()
        {
            EventManager.OnItemAddedToInventory += UpdateUI;
            EventManager.OnItemRemovedFromInventory += UpdateUI;
            EventManager.OnShopToggle += UpdateUI;
            CreateSlot(12);
        }

        protected void UpdateUI()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].RenderUI(null);
            }
            int k = 0;
            foreach (EquippableItem item in InventoryManager.instance.inventory)
            {
                itemSlots[k].RenderUI(item);
                k++;
            }
        }

        private void OnDestroy()
        {



            EventManager.OnItemAddedToInventory -= UpdateUI;
            EventManager.OnItemRemovedFromInventory -= UpdateUI;
            EventManager.OnShopToggle -= UpdateUI;
        }
    }
}