using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ShopButton : MonoBehaviour
    {
        private GameObject shopContainer;
        private Button shopButton;

        // Start is called before the first frame update
        private void Awake()
        {
            shopButton = GetComponent<Button>();
        }

        private void Start()
        {
            shopButton.onClick.AddListener(ToggleShop);
        }

        private void ToggleShop()
        {
            shopContainer = GameObject.Find("Shop");
            GameObject shopPanelGO = shopContainer.transform.Find("ShopPanel").gameObject;
            ShopUI shopUI = shopPanelGO.GetComponent<ShopUI>();
            shopUI.TogglePanel();
            shopUI.UpdateUI();
        }
    }
}