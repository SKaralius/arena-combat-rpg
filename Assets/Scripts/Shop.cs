using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
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
