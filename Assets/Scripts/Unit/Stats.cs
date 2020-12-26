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
            { EStats.Armor, "Armor" },
            { EStats.MoveSpeed, "Movement Speed" },
            { EStats.Health, "Health" },
            { EStats.Evasion, "Evasion" },
            { EStats.Critical, "Critical" },
            { EStats.AttackRange, "Attack Range" },
        };
        public static string GetStatName(EStats stat)
        {
            string name = "";
            StatNames.TryGetValue(stat, out name);
            return name;
        }
        public Stats()
        {
            ResetStats();
        }

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
            if (statNumber >= statsArray.Length)
            {
                Array.Resize(ref statsArray, statNumber + 1);
                return 0;
            }
            else
            {
                return statsArray[statNumber];
            }
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