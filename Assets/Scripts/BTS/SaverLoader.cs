using Inventory;
using System.Collections.Generic;
using TigerForge;
using Unit;
using UnityEngine;

public class SaverLoader : MonoBehaviour
{
    private EasyFileSave myFile;
    private GameObject Player;
    [SerializeField] private InventoryManager inventoryManager = null;
    [SerializeField] private GameObject inventoryPanel = null;
    private Gold gold;
    // TODO: Clean this up, cache components.

    #region Singleton logic

    public static SaverLoader instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    #endregion Singleton logic
        gold = inventoryPanel.GetComponentInChildren<Gold>();
        Player = GameObject.FindGameObjectWithTag("Player");
        myFile = new EasyFileSave("Items")
        {
            suppressWarning = false
        };
    }


    // Start is called before the first frame update
    private void Start()
    {
        myFile.Delete();
        if (myFile.FileExists())
        {
            if (myFile.Load())
            {
                // Inventory items
                List<EquippableItem> itemToAdd = (List<EquippableItem>)myFile.GetDeserialized("inventory", typeof(List<EquippableItem>));
                if (itemToAdd == null)
                {
                    MessageSystem.Print("Loaded Null");
                }
                else
                {
                    MessageSystem.Print("File Loaded Successfully");
                }
                foreach (EquippableItem item in itemToAdd)
                {
                    inventoryManager.AddItemToInventory(item);
                }
                // Equip items
                EquippableItem[] eqItemsToAdd = (EquippableItem[])myFile.GetDeserialized("eqItems", typeof(EquippableItem[]));
                if (eqItemsToAdd == null)
                {
                    MessageSystem.Print("Loaded Null");
                }
                else
                {
                    MessageSystem.Print("File Loaded Successfully");
                }
                foreach (EquippableItem eqItem in eqItemsToAdd)
                {
                    if (eqItem != null)
                    {
                        Player.GetComponent<PlayerEquippedItems>().ForceEquip(eqItem);
                    }
                }
                // Gold
                gold.ChangeGold(myFile.GetInt("gold", defaultValue: 0));
            }
            else
            {
                MessageSystem.Print("Load Failed!");
            }
        }
        myFile.Dispose();
    }

    public void SaveInventory()
    {
        myFile.AddSerialized("inventory", inventoryManager.inventory);
        myFile.AddSerialized("eqItems", Player.GetComponent<PlayerEquippedItems>().EquippedItemsArray);
        myFile.Add("gold", gold.Wealth);
        myFile.Save();
    }
}