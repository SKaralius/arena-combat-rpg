using UnityEngine;

namespace Inventory
{
    public class ShopUI : PanelUI
    {
        [SerializeField] private InventoryUIManager inventoryUIManager = null;
        [SerializeField] private GameObject inventoryButtonGO = null;
        private void Awake()
        {
            CreateSlot(12);            
        }

        public void UpdateUI()
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
        public override void TogglePanel()
        {
            base.TogglePanel();
            if (gameObject.activeSelf == true)
            {
                inventoryUIManager.gameObject.SetActive(true);
                inventoryUIManager.UpdateUI();
                inventoryButtonGO.SetActive(false);
            } else
            {
                inventoryUIManager.gameObject.SetActive(false);
            }
        }
    }
}