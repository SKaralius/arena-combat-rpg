﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquippableItem : IItem, IStats, IEquipable
{
    // IItem
    public string Name { get; }
    public (string, string) SpriteCategoryLabel { get; }
    public float SellPrice { get; }
    public Skills.ESkills Skill;
    public void Sell()
    {
        Debug.Log($"{Name} is sold.");
    }
    // IEquipable
    public EquipSlot Slot { get; }
    // Stats

    public float[] Stats { get; set; }

    public EquippableItem(EquipSlot slot, string _name, (string, string) spriteCategoryLabel, float _sellPrice, float _damage = 0, float _armor = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0, Skills.ESkills _skill = 0)
    {
        Slot = slot;
        SpriteCategoryLabel = spriteCategoryLabel;
        Stats = new float[6];
        Name = _name;
        SellPrice = _sellPrice;
        Skill = _skill;
        Stats[(int)EStats.Damage] = _damage;
        Stats[(int)EStats.Armor] = _armor;
        Stats[(int)EStats.MoveSpeed] = _moveSpeed;
        Stats[(int)EStats.Health] = _health;
        Stats[(int)EStats.HealthRegen] = _healthRegen;
        Stats[(int)EStats.Evasion] = _evasion;
    }
}
