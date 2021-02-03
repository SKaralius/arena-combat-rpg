using TMPro;
using UnityEngine;

namespace Inventory
{
    public class Gold : MonoBehaviour
    {
        private TextMeshProUGUI wealthText;
        public int Wealth { get; private set; } = 0;

        private void Awake()
        {
            wealthText = GetComponent<TextMeshProUGUI>();
        }

        public void ChangeGold(int amount)
        {
            Wealth += amount;
            if (Wealth < 0)
                Wealth = 0;
            if (wealthText)
                wealthText.text = Wealth.ToString();
        }
    } 
}