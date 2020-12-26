using UnityEngine;
using System.Collections.Generic;

namespace Unit
{
    public class UnitStats : MonoBehaviour
    {
        private Stats Stats { get; set; } = new Stats();
        public Stats StatModifiers { get; private set; } = new Stats();
        private EquippedItems eqItems;

        private void Awake()
        {
            eqItems = GetComponent<EquippedItems>();
            Stats.SetStat(EStats.Damage, 5);
            Stats.SetStat(EStats.Armor, 5);
            Stats.SetStat(EStats.MoveSpeed, 2);
            Stats.SetStat(EStats.Health, 100);
            Stats.SetStat(EStats.Evasion, 15);
            Stats.SetStat(EStats.Critical, 5);
            Stats.SetStat(EStats.AttackRange, 7);
        }

        public float GetStat(EStats stat)
        {
            float retrievedStat;
            if (stat == EStats.Armor || stat == EStats.Evasion || stat == EStats.Critical)
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
            StatModifiers.ResetStats();
        }
        private float GetMultiplicativeStat(EStats stat)
        {
            List<float> allStatInstances = new List<float>();
            foreach (EquippableItem item in eqItems.EquippedItemsArray)
            {
                if (item != null)
                    allStatInstances.Add(item.Stats.GetStat(stat));
            }
            allStatInstances.Add(Stats.GetStat(stat));
            allStatInstances.Add(StatModifiers.GetStat(stat));
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
            float tempStat = Stats.GetStat(stat);
            foreach (EquippableItem item in eqItems.EquippedItemsArray)
            {
                if (item != null)
                    tempStat += item.Stats.GetStat(stat);
            }
            tempStat += StatModifiers.GetStat(stat);
            return tempStat;
        }
    }
}