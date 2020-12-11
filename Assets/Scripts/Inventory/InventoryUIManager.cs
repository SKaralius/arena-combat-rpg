namespace Inventory
{
    public class InventoryUIManager : PanelUI
    {
        private InventoryManager inventoryManager;
        private void Awake()
        {
            inventoryManager = GetComponent<InventoryManager>();
            CreateSlot(12);
        }

        public void UpdateUI()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].RenderUI(null);
            }
            int k = 0;
            foreach (EquippableItem item in inventoryManager.inventory)
            {
                itemSlots[k].RenderUI(item);
                k++;
            }
        }
        public override void TogglePanel()
        {
            base.TogglePanel();
            UpdateUI();
        }
    }
}