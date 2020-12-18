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
        [SerializeField] protected Image itemImage;
        [SerializeField] protected Button button;
        [SerializeField] protected Button tooltipButton;
        [SerializeField] protected TextMeshProUGUI itemTooltipText;
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
        protected virtual void RenderTooltip(EquippableItem item)
        {
            itemTooltipText.text = "";
            itemTooltipText.text += item.Name;
            itemTooltipText.text += "\n";
            if (Skills.instance && item.Skill != ESkills.None)
            {
                itemTooltipText.text += Skills.instance.skillsList[item.Skill].Name;
                itemTooltipText.text += "\n";
            }
            foreach (float stat in item.Stats)
            {
                itemTooltipText.text += stat.ToString();
                itemTooltipText.text += "\n";
            }
        }
        protected void PrepareTooltipButton()
        {
            tooltipButton.gameObject.SetActive(true);
            tooltipButton.onClick.AddListener(() => tooltipManager.tooltip.SetActive(true));
        }
        protected void SetUpButton(UnityAction action)
        {
            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
        protected void SetSlotEmpty()
        {
            tooltipButton.gameObject.SetActive(false);
            //itemName.text = "Empty";
            //itemImage.sprite = null;
            button.gameObject.SetActive(false);
        }
    } 
}