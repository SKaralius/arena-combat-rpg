using TMPro;
using UnityEngine;

namespace Inventory
{
    public class Gold : MonoBehaviour
    {
        private TextMeshProUGUI wealthText;
        private int _wealth = 0;

        public int Wealth
        {
            get => _wealth;
            set
            {
                ChangeGold(value);
            }
        }

        private void Start()
        {
            wealthText = GetComponent<TextMeshProUGUI>();
        }

        private void ChangeGold(int amount)
        {
            _wealth += amount;
            wealthText.text = Wealth.ToString();
        }
    } 
}