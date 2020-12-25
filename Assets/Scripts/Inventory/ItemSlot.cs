using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Battle;
using Unit;

namespace Inventory
{
    public abstract class ItemSlot : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI itemName;
        [SerializeField] protected Button button;
        [SerializeField] protected Button tooltipButton;
        [SerializeField] protected Image itemSprite;
        [SerializeField] protected InventorySpriteManager inventorySpriteManager;
        protected UIContainer tooltipManager;
        protected void Awake()
        {
            GameObject UIGO = GameObject.Find("UI");
            tooltipManager = UIGO.GetComponent<UIContainer>();
        }
        public abstract void RenderUI(EquippableItem item);

        protected virtual void RenameButton(string name)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
        private void RenderTooltip(EquippableItem item)
        {
            tooltipManager.textComp.text = "";
            tooltipManager.textComp.text += item.Name;
            tooltipManager.textComp.text += "\n";
            if (Skills.instance && item.Skill != ESkills.None)
            {
                tooltipManager.textComp.text += "Skill: ";
                tooltipManager.textComp.text += Skills.instance.skillsList[item.Skill].Name;
                tooltipManager.textComp.text += "\n";
            }
            AddStatLine("Damage", item.Stats[(int)EStats.Damage]);
            AddStatLine("Armor", item.Stats[(int)EStats.Armor]);
            AddStatLine("Speed", item.Stats[(int)EStats.MoveSpeed]);
            AddStatLine("Health", item.Stats[(int)EStats.Health]);
            AddStatLine("Evasion", item.Stats[(int)EStats.Evasion]);

            tooltipManager.inventorySpriteManager.CreateAndDisplaySprite(item);
        }
        protected void SetUpSprite(EquippableItem item)
        {
            inventorySpriteManager.CreateAndDisplaySprite(item);

        }
        protected void SetUpTooltipButton(EquippableItem item)
        {
            tooltipButton.gameObject.SetActive(true);
            tooltipButton.onClick.AddListener(() => {
                tooltipManager.tooltip.SetActive(true);
                RenderTooltip(item);
                }
            );
        }
        protected void SetUpButton(UnityAction action)
        {
            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
        protected void SetSlotEmpty()
        {
            inventorySpriteManager.HideSprite();

            tooltipButton.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
        }
        private void AddStatLine(string statName, float value)
        {
            tooltipManager.textComp.text += statName;
            tooltipManager.textComp.text += ": ";
            tooltipManager.textComp.text += Mathf.Floor(value).ToString();
            tooltipManager.textComp.text += "\n";
        }
    } 
}