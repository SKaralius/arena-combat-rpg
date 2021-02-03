using UnityEngine;
using System.Collections.Generic;

namespace Unit
{
    public class UnitStats : MonoBehaviour
    {
        private Stats Stats { get; set; } = new Stats();
        public Stats StatModifiers { get; private set; } = new Stats();
        private EquippedItems eqItems;

        private readonly float healthBalanceRatio = 2f;
        private readonly float damageBalanceRatio = 0.5f;

        private void Awake()
        {
            eqItems = GetComponent<EquippedItems>();
            Stats.SetStat(EStats.Damage, 10 / damageBalanceRatio);
            Stats.SetStat(EStats.Accuracy, 0);
            Stats.SetStat(EStats.Armor, 0);
            Stats.SetStat(EStats.MoveSpeed, 15);
            Stats.SetStat(EStats.Health, 100 / healthBalanceRatio);
            Stats.SetStat(EStats.Evasion, 15);
            Stats.SetStat(EStats.Critical, 15);
            Stats.SetStat(EStats.AttackRange, 7);
        }

        public float GetStat(EStats stat)
        {
            float retrievedStat;
            switch (stat)
            {
                case EStats.Damage:
                    retrievedStat = GetAdditiveStat(stat) * damageBalanceRatio;
                    break;
                case EStats.Health:
                    retrievedStat = GetAdditiveStat(stat) * healthBalanceRatio;
                    break;
                default:
                    retrievedStat = GetAdditiveStat(stat);
                    break;
            }
            if (retrievedStat < 0)
                retrievedStat = 0;
            return retrievedStat;
        }
        public void ResetModifiers()
        {
            StatModifiers.ResetStats();
        }
        //private float GetMultiplicativeStat(EStats stat)
        //{
        //    List<float> allStatInstances = new List<float>();
        //    foreach (EquippableItem item in eqItems.EquippedItemsArray)
        //    {
        //        if (item != null)
        //            allStatInstances.Add(item.Stats.GetStat(stat));
        //    }
        //    allStatInstances.Add(Stats.GetStat(stat));
        //    allStatInstances.Add(StatModifiers.GetStat(stat));
        //    float reverseStatProduct = 1;
        //    foreach (float statInstance in allStatInstances)
        //    {
        //        if (statInstance != 0)
        //            reverseStatProduct *= (1 - statInstance / 100);
        //    }
        //    return (1 - reverseStatProduct) * 100;
        //}
        private float GetAdditiveStat(EStats stat)
        {
            float tempStat = Stats.GetStat(stat);
            foreach (EquippableItem item in eqItems.EquippedItemsArray)
            {
                if (item != null)
                {
                    Stats itemStats = item.Stats;
                    float statOnItem = itemStats.GetStat(stat);
                    tempStat += statOnItem;
                }
            }
            tempStat += StatModifiers.GetStat(stat);
            return tempStat;
        }
    }
}