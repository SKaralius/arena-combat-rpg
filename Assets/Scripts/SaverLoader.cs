using Inventory;
using System.Collections.Generic;
using TigerForge;
using Unit;
using UnityEngine;

public class SaverLoader : MonoBehaviour
{
    private EasyFileSave myFile;
    private GameObject Player;
    private InventoryManager inventoryManager;
    // TODO: Clean this up, cache components.

    #region Singleton logic

    public static SaverLoader instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion Singleton logic

    // Start is called before the first frame update
    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryPanel").GetComponent<InventoryManager>();
        myFile = new EasyFileSave("Items")
        {
            suppressWarning = false
        };
        //myFile.Delete();
        Player = GameObject.FindGameObjectWithTag("Player");
        if (myFile.FileExists())
        {
            if (myFile.Load())
            {
                // Inventory items
                List<EquippableItem> itemToAdd = myFile.GetBinary("inventory") as List<EquippableItem>;
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
                    inventoryManager.AddItemToInventory(item, Player.GetHashCode());
                }
                // Equip items
                EquippableItem[] eqItemsToAdd = myFile.GetBinary("eqItems") as EquippableItem[];
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
                        Player.GetComponent<PlayerEquippedItems>().Equip(eqItem);
                    }
                }
                // Gold
                EventManager.GoldChanged(myFile.GetInt("gold", defaultValue: 0));
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
        myFile.AddBinary("inventory", inventoryManager.inventory);
        myFile.AddBinary("eqItems", Player.GetComponent<PlayerEquippedItems>().equippedItems);
        myFile.Add("gold", Gold.instance.Wealth);
        myFile.Save();
    }
}