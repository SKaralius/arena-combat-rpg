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
    [SerializeField] private Shop shop = null;
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
                LoadInventory();
                LoadEquipment();
                LoadShop();
                // Gold
                gold.ChangeGold(myFile.GetInt("gold", defaultValue: 5000));
                // Encounter
                gameManager.nextEncounterNumber = myFile.GetInt("encounter", defaultValue: 1);
            }
            else
            {
                MessageSystem.Print("Load Failed!");
            }
        } else
        {
            gold.ChangeGold(5000);
            shop.GenerateItems();
            SaveInventory();
        }
        myFile.Dispose();
    }

    public void SaveInventory()
    {
        myFile.AddSerialized("inventory", inventoryManager.inventory);
        myFile.AddSerialized("eqItems", Player.GetComponent<PlayerEquippedItems>().EquippedItemsArray);
        myFile.AddSerialized("shop", shop.inventory);
        myFile.Add("gold", gold.Wealth);
        myFile.Add("encounter", gameManager.nextEncounterNumber);
        myFile.Save();
    }

    private void LoadInventory()
    {
        List<EquippableItem> itemToAdd = (List<EquippableItem>)myFile.GetDeserialized("inventory", typeof(List<EquippableItem>));
        if (itemToAdd == null)
        {
            MessageSystem.Print("Loaded Null");
            return;
        }
        else
        {
            MessageSystem.Print("File Loaded Successfully");
        }
        foreach (EquippableItem item in itemToAdd)
        {
            inventoryManager.AddItemToInventory(item);
        }
    }

    private void LoadEquipment()
    {
        EquippableItem[] eqItemsToAdd = (EquippableItem[])myFile.GetDeserialized("eqItems", typeof(EquippableItem[]));
        if (eqItemsToAdd == null)
        {
            MessageSystem.Print("Loaded Null");
            return;
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
    }
    private void LoadShop()
    {
        List<EquippableItem> shopItemsToAdd = (List<EquippableItem>)myFile.GetDeserialized("shop", typeof(List<EquippableItem>));
        if (shopItemsToAdd == null)
        {
            MessageSystem.Print("Loaded Null");
            return;
        }
        else
        {
            MessageSystem.Print("File Loaded Successfully");
        }
        foreach (EquippableItem item in shopItemsToAdd)
        {
            shop.AddItemToShop(item);
        }
    }
}