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
        if (tier < 1)
            tier = 1;
        float score = tier * 100;
        // Get all item set names
        // Define item tiers
        // Compare score to tier, or replace score with tier
        // Or add tier and generate score between those values
        // Sprite category label
        // Sell price is derived from score

        // Attack range can be always 0 for now, and the basic attack range will be shared with all weapons.

        // Need to generate stats and apply score formula
        List<float> statRatios = DistributeSumBetweenNRandomNumbers(Enum.GetNames(typeof(EStats)).Length);
        List<float> statRatiosXScore = new List<float>();
        foreach (float stat in statRatios)
        {
            statRatiosXScore.Add(stat * score);
        }
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
            _moveSpeed: statRatiosXScore[(int)EStats.MoveSpeed],
            _health: statRatiosXScore[(int)EStats.Health],
            _evasion: statRatiosXScore[(int)EStats.Evasion] / multiplicativeStatPenalty,
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
            return "None";
    }
}
