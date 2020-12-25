using UnityEngine;
using System.Collections.Generic;

namespace Unit
{
    public class UnitStats : MonoBehaviour, IStats
    {
        // { Damage, Armor, MoveSpeed, Health, Evasion, AttackRange }
        public float[] Stats { get; set; } = { 5, 5, 5, 100, 15, 7 };
        public float[] StatModifiers { get; set; } = { 0, 0, 0, 0, 0, 0 };
        private EquippedItems eqItems;

        private void Awake()
        {
            eqItems = GetComponent<EquippedItems>();
        }

        public float GetStat(EStats stat)
        {
            float retrievedStat;
            if (stat == EStats.Armor || stat == EStats.Evasion)
            {
                retrievedStat = GetMultiplicativeStat(stat);
            } else
            {
                retrievedStat = GetAdditiveStat(stat);
            }
            if (retrievedStat < 0)
                retrievedStat = 0;
            return retrievedStat;
        }
        public void ResetModifiers()
        {
            StatModifiers = new float[] {0,0,0,0,0,0};
        }
        private float GetMultiplicativeStat(EStats stat)
        {
            List<float> allStatInstances = new List<float>();
            foreach (IStats item in eqItems.EquippedItemsArray)
            {
                if (item != null)
                    allStatInstances.Add(item.Stats[(int)stat]);
            }
            allStatInstances.Add(Stats[(int)stat]);
            allStatInstances.Add(StatModifiers[(int)stat]);
            float reverseStatProduct = 1;
            foreach (float statInstance in allStatInstances)
            {
                if (statInstance != 0)
                    reverseStatProduct *= (1 - statInstance / 100);
            }
            return (1 - reverseStatProduct) * 100;
        }
        private float GetAdditiveStat(EStats stat)
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
    }
}