using UnityEngine;

namespace Unit
{
    public class UnitStats : MonoBehaviour, IStats
    {
        public float[] Stats { get; set; } = { 5, 5, 5, 100, 5, 50, 5 };
        private EquippedItems eqItems;

        private void Awake()
        {
            eqItems = GetComponent<EquippedItems>();
        }

        public float GetStat(EStats stat)
        {
            float tempStat = Stats[(int)stat];
            foreach (IStats item in eqItems.equipedItems)
            {
                if (item != null)
                    tempStat += item.Stats[(int)stat];
            }
            return tempStat;
        }
    }
}