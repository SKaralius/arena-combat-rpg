using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory
{
    public class InventoryUIManager : MonoBehaviour
    {
        public GameObject inventorySlotPrefab;
        private InventorySlot[] inventorySlots = new InventorySlot[12];
        void Awake()
        {
            EventManager.OnItemAddedToInventory += UpdateUI;
            EventManager.OnItemRemovedFromInventory += UpdateUI;
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i] = Instantiate(inventorySlotPrefab, transform.GetChild(1)).GetComponent<InventorySlot>();
            }
        }
        private void UpdateUI()
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].RenderUI(null);
            }
            int k = 0;
            foreach (EquippableItem item in InventoryManager.instance.inventory)
            {
                inventorySlots[k].RenderUI(item);
                k++;
            }
        }
        public void ToggleInventory()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        private void OnDestroy()
        {
            EventManager.OnItemAddedToInventory -= UpdateUI;
            EventManager.OnItemRemovedFromInventory -= UpdateUI;
        }
    }
}