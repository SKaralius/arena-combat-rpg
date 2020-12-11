using Inventory;
using Unit;
using UnityEngine;

public class InventoryEquipmentMediator : MonoBehaviour
{
    private GameObject Player;
    private EquippedItems eqItems;
    private InventoryManager inventoryManager;
    private InventoryUIManager InventoryUIManager;

    #region Singleton logic

    public static InventoryEquipmentMediator instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion Singleton logic

    private void Start()
    {
        GameObject inventoryPanelGO = GameObject.Find("InventoryPanel");
        inventoryManager = inventoryPanelGO.GetComponent<InventoryManager>();
        InventoryUIManager = inventoryPanelGO.GetComponent<InventoryUIManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        eqItems = Player.GetComponent<EquippedItems>();
    }

    public void Equip(EquippableItem item)
    {
        // If equip slot is empty
        if (eqItems.equippedItems[(int)item.Slot] == null)
        {
            eqItems.Equip(item);
            inventoryManager.RemoveItemFromInventory(item, Player.GetHashCode());
        }
        // If there is an item already equiped
        else
        {
            EquippableItem previouslyEquippedItem;
            previouslyEquippedItem = eqItems.equippedItems[(int)item.Slot];
            eqItems.Unequip(previouslyEquippedItem.Slot);
            // Sets the previously equipped item, to the slot in inventory.
            eqItems.Equip(item);
            inventoryManager.inventory[inventoryManager.inventory.IndexOf(item)] = previouslyEquippedItem;
        }
        GameObject battleSystemGO = GameObject.Find("BattleSystem");
        if (battleSystemGO)
        {
            BattleSystem battleSystem = battleSystemGO.GetComponent<BattleSystem>();
            battleSystem.OnItemEquipButton(item);
        }
            InventoryUIManager.UpdateUI();
    }

    public void Unequip(EquippableItem item)
    {
        if (inventoryManager.inventory.Count < 12)
        {
            eqItems.Unequip(item.Slot);
            inventoryManager.AddItemToInventory(item, Player.GetHashCode());
        }
        else
        {
            MessageSystem.Print("Inventory is full");
        }
    }
}