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

public enum EStats
{ Damage, Armor, MoveSpeed, Health, HealthRegen, Evasion }
public interface IStats
{
    float[] Stats { get; set; }
}
public class EquipableItem : IItem, IStats, IEquipable
{
    // IItem
    public string Name { get; }
    public float SellPrice { get; }
    public void Sell()
    {
        Debug.Log($"{Name} is sold.");
    }
    // IEquipable
    public EquipSlot Slot { get; }
    // Stats

    public float[] Stats { get; set; }

    public EquipableItem(EquipSlot slot ,string _name, float _sellPrice, float _damage = 0, float _armor = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0)
    {
        Slot = slot;
        Stats = new float[6];
        Name = _name;
        SellPrice = _sellPrice;
        Stats[(int)EStats.Damage] = _damage;
        Stats[(int)EStats.Armor] = _armor;
        Stats[(int)EStats.MoveSpeed] = _moveSpeed;
        Stats[(int)EStats.Health] = _health;
        Stats[(int)EStats.HealthRegen] = _healthRegen;
        Stats[(int)EStats.Evasion] = _evasion;
    }
}
//public class Head : EquipableItem
//{
//    public override EquipSlot Slot { get; } = EquipSlot.Head;

//    public Head(string _name, float _sellPrice, float _damage = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0) : base(_name, _sellPrice, _damage, _moveSpeed, _health, _healthRegen, _evasion)
//    {
//    }
//}
//public class RightWeapon : EquipableItem
//{
//    public override EquipSlot Slot { get; } = EquipSlot.RightWeapon;

//    public RightWeapon(string _name, float _sellPrice, float _damage = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0) : base(_name, _sellPrice, _damage, _moveSpeed, _health, _healthRegen, _evasion)
//    {
//    }
//}
//public class LeftWeapon : EquipableItem
//{
//    public override EquipSlot Slot { get; } = EquipSlot.LeftWeapon;

//    public LeftWeapon(string _name, float _sellPrice, float _damage = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0) : base(_name, _sellPrice, _damage, _moveSpeed, _health, _healthRegen, _evasion)
//    {
//    }
//}
//public class Chest : EquipableItem
//{
//    public override EquipSlot Slot { get; } = EquipSlot.Chest;

//    public Chest(string _name, float _sellPrice, float _damage = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0) : base(_name, _sellPrice, _damage, _moveSpeed, _health, _healthRegen, _evasion)
//    {
//    }
//}

//public class Legs : EquipableItem
//{
//    public override EquipSlot Slot { get; } = EquipSlot.Legs;

//    public Legs(string _name, float _sellPrice, float _damage = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0) : base(_name, _sellPrice, _damage, _moveSpeed, _health, _healthRegen, _evasion)
//    {
//    }
//}