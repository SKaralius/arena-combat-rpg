using UnityEngine;

namespace Inventory
{
    public class ShopUI : PanelUI
    {
        private InventoryUIManager inventoryUIManager;
        private void Awake()
        {
            inventoryUIManager = GameObject.Find("InventoryPanel").GetComponent<InventoryUIManager>();
            EventManager.OnItemAddedToShop += UpdateUI;
            EventManager.OnItemRemovedFromShop += UpdateUI;
            CreateSlot(12);
        }

        protected void UpdateUI()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].RenderUI(null);
            }
            int k = 0;
            foreach (EquippableItem item in GetComponent<Shop>().inventory)
            {
                itemSlots[k].RenderUI(item);
                k++;
            }
        }

        private void OnDestroy()
        {
            EventManager.OnItemAddedToShop -= UpdateUI;
            EventManager.OnItemRemovedFromShop -= UpdateUI;
        }
        public override void TogglePanel()
        {
            base.TogglePanel();
            inventoryUIManager.UpdateUI();
        }
    }
}