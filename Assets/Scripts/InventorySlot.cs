using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private GameObject player;
    public IItem item = null;
    public TextMeshProUGUI itemName;
    public Button equip;
    public Button unequip;
    private EquippedItems eqItems;
    private BattleSystem battleSystem;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        battleSystem = GameObject.Find("GameManager").GetComponent<BattleSystem>();
        eqItems = player.GetComponent<EquippedItems>();
    }
    public void RenderUI()
    {
        if (item != null)
        {
            itemName.text = item.Name;
        } else
        {
            itemName.text = "Empty";
            equip.gameObject.SetActive(false);
        }
        if (item is IEquipable equipable)
        {
            if (eqItems.IsItemEquipped(equipable))
            {
                unequip.gameObject.SetActive(true);
                unequip.onClick.RemoveAllListeners();
                unequip.onClick.AddListener(() => { eqItems.Unequip(equipable.Slot); });
            }
            else
            {
                equip.gameObject.SetActive(true);
                equip.onClick.RemoveAllListeners();
                equip.onClick.AddListener(() => { battleSystem.OnItemEquipButton(equipable); });
            }
        }
    }
}
