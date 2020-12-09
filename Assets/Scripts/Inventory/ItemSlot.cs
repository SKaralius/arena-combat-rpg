using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public abstract class ItemSlot : MonoBehaviour
    {
        public TextMeshProUGUI itemName;
        public Image itemImage;
        public Button button;

        // Start is called before the first frame update
        public abstract void RenderUI(EquippableItem item);

        protected virtual void RenameButton(string name)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
        }
    } 
}