using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Arena : MonoBehaviour
{
    public void StartFight()
    {
        GameObject inventoryPanel = GameObject.Find("InventoryPanel");
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);

        GameObject shopPanel = GameObject.Find("ShopPanel");
        if (shopPanel != null && shopPanel.activeSelf)
            shopPanel.transform.Find("CloseButton").GetComponent<Button>().onClick.Invoke();

        GameObject equipmentPanel = GameObject.Find("EquipmentPanel");
        if (equipmentPanel != null)
            equipmentPanel.SetActive(false);

        SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("TownScene");
    }
}