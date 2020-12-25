using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unit;

public static class ItemGenerator
{
    public static EquippableItem GenerateItem(int tier, EquipSlot? slot = null)
    {
        if (slot == null)
        {
            slot = GetRandomEquipSlot();
        }
        float score = tier * 100;

        int numberOfStats = Enum.GetNames(typeof(EStats)).Length;
        List<float> statRatios = DistributeSumBetweenNRandomNumbers(UnityEngine.Random.Range(1, numberOfStats));
        List<float> statRatiosXScore = new List<float>();
        for (int i = 0; i < numberOfStats; i++)
        {
            if (i >= statRatios.Count)
                statRatiosXScore.Add(0);
            else
                statRatiosXScore.Add(Mathf.Floor(statRatios[i] * score));
        }
        statRatiosXScore.Shuffle();
        int multiplicativeStatPenalty = 3;

        string category = EquipSlotToSpriteCategory((EquipSlot)slot);
        string label = GetLabelFromTier(tier);

        return new EquippableItem(
            slot: (EquipSlot)slot,
            _name: label,
            spriteCategoryLabel: (category, label),
            _sellPrice: 0,
            _attackRange: 0,
            _damage: statRatiosXScore[(int)EStats.Damage],
            _armor: statRatiosXScore[(int)EStats.Armor] / multiplicativeStatPenalty,
            _moveSpeed: statRatiosXScore[(int)EStats.MoveSpeed] / multiplicativeStatPenalty,
            _health: statRatiosXScore[(int)EStats.Health],
            _evasion: statRatiosXScore[(int)EStats.Evasion] / multiplicativeStatPenalty,
            _critical: statRatiosXScore[(int)EStats.Critical] / multiplicativeStatPenalty,
            _skill: RollForSkill()
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
    private static ESkills RollForSkill()
    {
        if (UnityEngine.Random.Range(0, 100) < 25)
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
            return "First";
        else
            return "First";
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
