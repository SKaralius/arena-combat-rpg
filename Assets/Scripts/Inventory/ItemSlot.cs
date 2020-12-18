using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unit;

namespace Inventory
{
    public abstract class ItemSlot : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI itemName;
        [SerializeField] protected Button button;
        [SerializeField] protected Button tooltipButton;
        [SerializeField] protected Image itemSprite;
        protected TooltipManager tooltipManager;
        protected void Awake()
        {
            GameObject UIGO = GameObject.Find("UI");
            tooltipManager = UIGO.GetComponent<TooltipManager>();
        }

        // Start is called before the first frame update
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
                tooltipManager.textComp.text += Skills.instance.skillsList[item.Skill].Name;
                tooltipManager.textComp.text += "\n";
            }
            foreach (float stat in item.Stats)
            {
                tooltipManager.textComp.text += stat.ToString();
                tooltipManager.textComp.text += "\n";
            }
        }
        protected void SetUpSprite(EquippableItem item)
        {
            Color temp = itemSprite.color;
            temp.a = 1;
            itemSprite.color = temp;
            itemSprite.sprite = ItemEquipGraphics.RetrieveSprite(item.SpriteCategoryLabel.Item1, item.SpriteCategoryLabel.Item2);
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
            Color temp = itemSprite.color;
            temp.a = 0;
            itemSprite.color = temp;
            itemSprite.sprite = null;
            tooltipButton.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
        }
    } 
}