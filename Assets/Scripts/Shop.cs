using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [HideInInspector]
    public List<EquippableItem> inventory = new List<EquippableItem>();

    #region Singleton logic
    public static Shop instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        AddItemToShop(new EquippableItem(EquipSlot.RightWeapon, "Cool Sword", ("Weapon", "Sword"), 69, 50));
    }

    public void AddItemToShop(EquippableItem item)
    {
        if (inventory.Count < 12)
        {
            inventory.Add(item);
            EventManager.ItemAddedToShop();
        }
        else
        {
            MessageSystem.Print("Inventory is full.");
        }
    }
    public void RemoveItemFromShop(EquippableItem item)
    {

    }
    public void ToggleShop()
    {
        MessageSystem.Print("Shop opened");
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void GenerateItems()
    {
        MessageSystem.Print("Items generated");
    }
}
