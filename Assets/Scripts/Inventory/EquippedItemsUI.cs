﻿using Unit;
using UnityEngine;
using TMPro;

namespace Inventory
{
    public class EquippedItemsUI : PanelUI
    {
        [SerializeField] private TextMeshProUGUI statDisplay;
        private GameObject player;
        private UnitStats unitStats;
        private EquippedItems eqItems;

        #region Singleton logic
        public static EquippedItemsUI instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        #endregion Singleton logic

            player = GameObject.FindGameObjectWithTag("Player");
            eqItems = player.GetComponent<EquippedItems>();
            unitStats = player.GetComponent<UnitStats>();
            EventManager.OnItemEquipped += UpdateUI;
            EventManager.OnItemUnequipped += UpdateUI;
            CreateSlot(5);
        }

        protected void UpdateUI(EquippableItem item, int who)
        {
            itemSlots[(int)item.Slot].RenderUI(eqItems.equipedItems[(int)item.Slot]);
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

        private void OnDestroy()
        {
            EventManager.OnItemEquipped -= UpdateUI;
            EventManager.OnItemUnequipped -= UpdateUI;
        }
    }
}