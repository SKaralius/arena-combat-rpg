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
            foreach (EStats stat in System.Enum.GetValues(typeof(EStats)))
            {
                statDisplay.text += unitStats.GetStat(stat).ToString();
                statDisplay.text += "\n";
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
    }
}