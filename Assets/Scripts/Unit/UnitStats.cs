using UnityEngine;

namespace Unit
{
    public class UnitStats : MonoBehaviour, IStats
    {
        public float[] Stats { get; set; } = { 5, 5, 5, 100, 5, 15, 5 };
        public float[] StatModifiers { get; set; } = { 0, 0, 0, 0, 0, 0, 0 };
        private EquippedItems eqItems;

        private void Awake()
        {
            eqItems = GetComponent<EquippedItems>();
        }

        public float GetStat(EStats stat)
        {
            float tempStat = Stats[(int)stat];
            foreach (IStats item in eqItems.EquippedItemsArray)
            {
                if (item != null)
                    tempStat += item.Stats[(int)stat];
            }
            tempStat += StatModifiers[(int)stat];
            return tempStat;
        }
        public void ResetModifiers()
        {
            StatModifiers = new float[] {0,0,0,0,0,0,0};
        }
    }
}