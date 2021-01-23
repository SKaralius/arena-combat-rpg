using Inventory;
using System.Collections.Generic;
using TigerForge;
using Unit;
using UnityEngine;

public class SaverLoader : MonoBehaviour
{
    private EasyFileSave myFile;
    private GameObject Player;
    private GameManager gameManager;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myFile = new EasyFileSave("Items")
        {
            suppressWarning = false
        };
    }


    // Start is called before the first frame update
    private void Start()
    {
        //myFile.Delete();
        if (myFile.FileExists())
        {
            if (myFile.Load())
            {
                #region Inventory Items
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
                #endregion Inventory Items
                #region Equipped Items
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
                #endregion Equipped Items
                // Gold
                gold.ChangeGold(myFile.GetInt("gold", defaultValue: 0));
                // Encounter
                gameManager.nextEncounterNumber = myFile.GetInt("encounter", defaultValue: 1);
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
        myFile.Add("encounter", gameManager.nextEncounterNumber);
        myFile.Save();
    }
}