using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipSlot
{
    Head, Chest, Legs, LeftWeapon, RightWeapon
}
public interface IItem
{
    string Name { get; }
        
    float SellPrice { get; }
    void Sell();
}
public interface IEquipable
{
    EquipSlot Slot { get; }
}
public interface IStats
{
    float Damage { get; }
    float Armor { get; }
    float MoveSpeed { get; }
    float Health { get; }
    float HealthRegen { get; }
}
public class Weapon : IItem, IEquipable
{
    // IItem
    public string Name { get; }
    public float SellPrice { get; }
    public void Sell()
    {
        Debug.Log($"{Name} is sold.");
    }
    // IEquipable
    public EquipSlot Slot { get; } = EquipSlot.RightWeapon;

    // Weapon

    public float Damage { get; }

    public Weapon(string _name, float _sellPrice, float _damage)
    {
        Name = _name;
        SellPrice = _sellPrice;
        Damage = _damage;
    }
}
