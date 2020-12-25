using TMPro;
using UnityEngine;

namespace Inventory
{
    public class Gold : MonoBehaviour
    {
        private TextMeshProUGUI wealthText;
        public int Wealth { get; private set; }

        private void Awake()
        {
            wealthText = GetComponent<TextMeshProUGUI>();
        }

        public void ChangeGold(int amount)
        {
            Wealth += amount;
            if (wealthText)
                wealthText.text = Wealth.ToString();
        }
    } 
}