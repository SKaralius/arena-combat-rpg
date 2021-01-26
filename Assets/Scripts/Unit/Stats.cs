using System.Collections.Generic;
using System;

namespace Unit
{
    public class Stats
    {
        private static readonly Dictionary<EStats, string> StatNames
            = new Dictionary<EStats, string>
        {
            { EStats.Damage, "Damage" },
            { EStats.Accuracy, "Accuracy" },
            { EStats.Armor, "Armor" },
            { EStats.MoveSpeed, "Movement Speed" },
            { EStats.Health, "Health" },
            { EStats.Evasion, "Evasion" },
            { EStats.Critical, "Critical" },
            { EStats.AttackRange, "Attack Range" },
        };
        public static string GetStatName(EStats stat)
        {
            StatNames.TryGetValue(stat, out string name);
            return name;
        }
        public Stats()
        {
            ResetStats();
        }
        // TODO: Need this to be public for serializer, but stats shouldnt be retrieved directly from the array.
        // Is the only solution to edit the library to use the methods?
        public float[] statsArray = new float[EStats.GetNames(typeof(EStats)).Length];

        public void ResetStats()
        {
            for (int i = 0; i < EStats.GetNames(typeof(EStats)).Length; i++)
            {
                statsArray[i] = 0;
            }
        }
        public float GetStat(EStats stat)
        {
            int statNumber = (int)stat;
            float statValue;
            if (statNumber >= statsArray.Length)
            {
                Array.Resize(ref statsArray, statNumber + 1);
                return 0;
            }
            else
            {
                statValue = statsArray[statNumber];
            }
            return statValue;
        }        
        public void SetStat(EStats stat, float value)
        {
            int statNumber = (int)stat;
            if (statNumber > statsArray.Length)
            {
                Array.Resize(ref statsArray, statNumber + 1);
            }
            statsArray[(int)stat] = value;
        }
        public float GetAllStatSum()
        {
            float statSum = 0;
            foreach (float statValue in statsArray)
            {
                statSum += statValue;
            }
            return statSum;
        }
    }
}