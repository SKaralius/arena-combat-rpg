using Inventory;
using UnityEngine;

namespace Unit
{
    public class PlayerEquippedItems : EquippedItems
    {
        [SerializeField] private EquippedItemsUI equippedItemsUI;
        protected InventoryManager inventoryManager;
        protected InventoryUIManager inventoryUIManager;
        [SerializeField] private GameObject inventoryPanelGO;

        new private void Awake()
        {
            base.Awake();
            inventoryManager = inventoryPanelGO.GetComponent<InventoryManager>();
            inventoryUIManager = inventoryPanelGO.GetComponent<InventoryUIManager>();
        }

        public override void Equip(EquippableItem item)
        {
            // If equip slot is empty
            if (EquippedItemsArray[(int)item.Slot] == null)
            {
                ForceEquip(item);
                inventoryManager.RemoveItemFromInventory(item);
            }
            // If there is an item already equiped
            else
            {
                EquippableItem previouslyEquippedItem;
                previouslyEquippedItem = EquippedItemsArray[(int)item.Slot];
                EquippedItemsArray[(int)item.Slot] = null;
                // Sets the previously equipped item, to the slot in inventory.
                ForceEquip(item);
                characterSkills.RemoveFromSkillList(previouslyEquippedItem);
                inventoryManager.inventory[inventoryManager.inventory.IndexOf(item)] = previouslyEquippedItem;
            }

            #region BattleSystem Turn Skip

            GameObject battleSystemGO = GameObject.Find("BattleSystem");
            if (battleSystemGO)
            {
                BattleSystem battleSystem = battleSystemGO.GetComponent<BattleSystem>();
                battleSystem.OnItemEquipButton(item);
            }

            #endregion BattleSystem Turn Skip

            #region Skill Manager Update

            SkillManager skillManager = GetComponent<SkillManager>();
            if (skillManager.enabled == true)
                skillManager.RenderSkillSlots();

            #endregion Skill Manager Update
            inventoryUIManager.UpdateUI();
            equippedItemsUI.UpdateUI(item.Slot);
        }

        public override void ForceEquip(EquippableItem item)
        {
            base.ForceEquip(item);
            if (equippedItemsUI.gameObject.activeSelf == true)
                equippedItemsUI.UpdateUI(item.Slot);
        }

        public void Unequip(EquipSlot Slot)
        {
            if (inventoryManager.inventory.Count < 12)
            {
                EquippableItem item = EquippedItemsArray[(int)Slot];
                EquippedItemsArray[(int)Slot] = null;
                MessageSystem.Print($"{item.Name} is unequiped on {gameObject.name}.");
                itemEquipGraphics.UnequipItem(Slot);
                characterSkills.RemoveFromSkillList(item);
                if (healthBar)
                    healthBar.UpdateHealthBar(GetComponent<Controller>().Health);
                inventoryManager.AddItemToInventory(item);
            }
            else
            {
                MessageSystem.Print("Inventory is full");
            }

            #region Skill Manager Update

            SkillManager skillManager = GetComponent<SkillManager>();
            if (skillManager.enabled == true)
                skillManager.RenderSkillSlots();

            #endregion Skill Manager Update
            equippedItemsUI.UpdateUI(Slot);
        }
    }
}