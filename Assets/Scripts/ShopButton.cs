using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

public class ShopButton : MonoBehaviour
{
    GameObject shopContainer;
    Button shopButton;
    // Start is called before the first frame update
    private void Awake()
    {
        shopButton = GetComponent<Button>();
    }

    void Start()
    {
        shopButton.onClick.AddListener(ToggleShop);
    }
    private void ToggleShop()
    {
        shopContainer = GameObject.Find("Shop");
        GameObject shopPanelGO = shopContainer.transform.Find("ShopPanel").gameObject;
        shopPanelGO.GetComponent<ShopUI>().TogglePanel();
    }
}
