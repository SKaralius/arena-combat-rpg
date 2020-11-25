using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsUI : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    private InventorySlot[] equipSlots = new InventorySlot[5];
    private EquippedItems eqItems;
    void Awake()
    {
        eqItems = GameObject.Find("PlayerUnit").GetComponent<EquippedItems>();
        EventManager.OnItemEquipped += UpdateUI;
        EventManager.OnItemUnequipped += UpdateUI;
        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i] = Instantiate(inventorySlotPrefab, transform.GetChild(1)).GetComponent<InventorySlot>();
        }
    }
    private void UpdateUI()
    {
        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].item = null;
            equipSlots[i].RenderUI();
        }
        int k = 0;
        foreach (IItem item in eqItems.equipedItems)
        {
            equipSlots[k].item = item;
            equipSlots[k].RenderUI();
            k++;
        }
    }
    public void ToggleEquippmentUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
