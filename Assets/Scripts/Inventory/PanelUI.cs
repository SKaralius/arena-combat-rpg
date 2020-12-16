using UnityEngine;

namespace Inventory
{
    public abstract class PanelUI : MonoBehaviour
    {
        [SerializeField]
        protected GameObject itemSlotPrefab;

        protected ItemSlot[] itemSlots;

        protected virtual void CreateSlot(int numberOfSlots)
        {
            itemSlots = new ItemSlot[numberOfSlots];
            for (int i = 0; i < numberOfSlots; i++)
            {
                itemSlots[i] = Instantiate(itemSlotPrefab, transform.GetChild(0)).GetComponent<ItemSlot>();
            }
        }

        public virtual void TogglePanel()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}