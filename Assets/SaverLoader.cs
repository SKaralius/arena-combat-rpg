using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using Inventory;

public class SaverLoader : MonoBehaviour
{
    EasyFileSave myFile;
    private GameObject Player;
    #region Singleton logic
    public static SaverLoader instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
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
                List<EquippableItem> itemToAdd = myFile.GetBinary("inventory") as List<EquippableItem>;
                if (itemToAdd == null)
                {
                    MessageSystem.Print("Loaded Null");
                } else
                {

                MessageSystem.Print("File Loaded Successfully");
                }
                foreach (EquippableItem item in itemToAdd)
                {
                    InventoryManager.instance.AddItemToInventory(item, Player.GetHashCode());
                }
            }
            else
            {
                MessageSystem.Print("Load Failed!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveInventory()
    {
        myFile.AddBinary("inventory", InventoryManager.instance.inventory);
        myFile.Save();
    }
}
