using Unit;
using UnityEngine;
using TMPro;

namespace Inventory
{
    public class EquippedItemsUI : PanelUI
    {
        [SerializeField] private TextMeshProUGUI statDisplay = default;
        private GameObject player;
        private UnitStats unitStats;
        private EquippedItems eqItems;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            eqItems = player.GetComponent<EquippedItems>();
            unitStats = player.GetComponent<UnitStats>();
            CreateSlot(5);
        }

        public void UpdateUI(EquipSlot slot)
        {
            itemSlots[(int)slot].RenderUI(eqItems.EquippedItemsArray[(int)slot]);
            RenderPlayerStats();
        }

        public void RenderPlayerStats()
        {
            statDisplay.text = "";
            foreach(EStats stat in EStats.GetValues(typeof(EStats)))
            {
                AddStatLine(Stats.GetStatName(stat), unitStats.GetStat(stat));
            }
        }
        protected override void CreateSlot(int numberOfSlots)
        {
            itemSlots = new ItemSlot[numberOfSlots];
            int i = 0;
            foreach (Transform itemLocation in transform.GetChild(0))
            {
                itemSlots[i] = Instantiate(itemSlotPrefab, itemLocation).GetComponent<ItemSlot>();
                UpdateUI((EquipSlot)i);
                i++;
            }
        }
        private void AddStatLine(string statName, float value)
        {
            statDisplay.text += statName;
            statDisplay.text += ": ";
            statDisplay.text += Mathf.Floor(value).ToString();
            statDisplay.text += "\n";
        }
    }
}