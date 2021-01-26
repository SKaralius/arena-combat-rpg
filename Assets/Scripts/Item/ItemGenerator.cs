using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public static class ItemGenerator
{
        public static int statScoreBaseValue = 99;
    public static EquippableItem GenerateItem(int encounterCount, EquipSlot? slot = null, ESkills skill = ESkills.None, bool guaranteeSkill = false)
    {
        float statRollDivisor = 100;
        if (slot == null)
        {
            slot = GetRandomEquipSlot();
        }
        (int, string) rollAndRarity = DecideRarity();

        float score = (statScoreBaseValue + encounterCount) * (rollAndRarity.Item1 / statRollDivisor);

        int numberOfStats = Enum.GetNames(typeof(EStats)).Length;
        List<float> statRatios = DistributeSumBetweenNRandomNumbers(UnityEngine.Random.Range(1, numberOfStats));
        List<float> statRatiosXScore = new List<float>();
        // -1 because attack range should not generate random values
        for (int i = 0; i < numberOfStats - 1; i++)
        {
            if (i >= statRatios.Count)
                statRatiosXScore.Add(0);
            else
                statRatiosXScore.Add(Mathf.Floor(statRatios[i] * score));
        }
        statRatiosXScore.Shuffle();

        string category = EquipSlotToSpriteCategory((EquipSlot)slot);
        string label = GetLabelFromTier(GameManager.instance.GetTier());

        Stats itemStats = new Stats();
        foreach (EStats stat in EStats.GetValues(typeof(EStats)))
        {
            if (stat == EStats.AttackRange)
                break;
            itemStats.SetStat(stat, statRatiosXScore[(int)stat]);
        }

        if (skill == ESkills.None)
            skill = RollForSkill(guaranteeSkill);

        return new EquippableItem(
            slot: (EquipSlot)slot,
            _name: label,
            spriteCategoryLabel: (category, label),
            _sellPrice: (int)Mathf.Ceil(score * 3),
            itemStats,
            _itemScore: rollAndRarity.Item1,
            _rarity: rollAndRarity.Item2,
            _skill: skill
            );
    }

    private static EquipSlot GetRandomEquipSlot()
    {
        int equipSlotCount = Enum.GetNames(typeof(EquipSlot)).Length;
        EquipSlot slot = (EquipSlot)UnityEngine.Random.Range(0, equipSlotCount);
        return slot;
    }

    private static List<float> DistributeSumBetweenNRandomNumbers(int dimensions)
    {
        List<float> randomNumberList = new List<float>
        {
            0,
            1
        };
        for (int i = 0; i < dimensions; i++)
        {
            randomNumberList.Add(UnityEngine.Random.Range(0f, 1f));
        }
        randomNumberList.Sort();
        List<float> differences = new List<float>();
        for (int i = 0; i < randomNumberList.Count; i++)
        {
            if (i + 1 == randomNumberList.Count)
                break;
            differences.Add(randomNumberList[i + 1] - randomNumberList[i]);
        }
        return differences;
    }

    private static ESkills RollForSkill(bool guaranteeSkill)
    {
        if (UnityEngine.Random.Range(0, 100) < 25 || guaranteeSkill)
        {
            return (ESkills)UnityEngine.Random.Range(4, Enum.GetNames(typeof(ESkills)).Length);
        }
        else
        {
            return ESkills.None;
        }
    }

    private static string EquipSlotToSpriteCategory(EquipSlot slot)
    {
        if (slot == EquipSlot.Chest)
            return "Chest";
        if (slot == EquipSlot.Head)
            return "Helmet";
        if (slot == EquipSlot.LeftWeapon || slot == EquipSlot.RightWeapon)
            return "Weapon";
        if (slot == EquipSlot.Legs)
            return "Pelvis";
        return "No such item";
    }

    private static string GetLabelFromTier(int tier)
    {
        if (tier == 1)
            return "Rags";
        else
            return "Rags";
    }

    private static (int, string) DecideRarity()
    {
        Dictionary<string, float> rarities = new Dictionary<string, float>
        {
            ["Common"] = 60,
            ["Rare"] = 30,
            ["Epic"] = 15,
            ["Legendary"] = 5
        };



        float percentRoll = UnityEngine.Random.Range(0, 100);
        string rarity = "Common";
        int roll = 0;
        if (percentRoll > rarities["Common"])
        {
            roll = UnityEngine.Random.Range(30, 45);
            rarity = "Common";
            return (roll, rarity);
        }
        if (percentRoll > rarities["Rare"] && percentRoll <= rarities["Common"])
        {
            roll = UnityEngine.Random.Range(45, 60);
            rarity = "Uncommon";
            return (roll, rarity);
        }
        if (percentRoll > rarities["Epic"] && percentRoll <= rarities["Rare"])
        {
            roll = UnityEngine.Random.Range(60, 75);
            rarity = "Rare";
            return (roll, rarity);
        }
        if (percentRoll > rarities["Legendary"] && percentRoll <= rarities["Epic"])
        {
            roll = UnityEngine.Random.Range(75, 90);
            rarity = "Epic";
            return (roll, rarity);
        }
        if (percentRoll <= rarities["Legendary"])
        {
            roll = UnityEngine.Random.Range(90, 101);
            rarity = "Legendary";
            return (roll, rarity);
        }
        return (roll, rarity);
    }

    //  Fisher-Yates shuffle
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}