using TMPro;
using UnityEngine;
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

    } 
}