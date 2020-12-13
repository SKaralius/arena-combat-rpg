using Unit;
using UnityEngine;

[System.Serializable]
public class EquippableItem : IItem, IStats, IEquipable
{
    // IItem
    public string Name { get; set; }

    public (string, string) SpriteCategoryLabel { get; set;  }
    public int SellPrice { get; set; }
    public ESkills Skill;

    // IEquipable
    public EquipSlot Slot { get; set; }

    // Stats

    public float[] Stats { get; set; }

    public EquippableItem(EquipSlot slot, string _name, (string, string) spriteCategoryLabel, int _sellPrice, float _attackRange = 0, float _damage = 0, float _armor = 0, float _moveSpeed = 0, float _health = 0, float _healthRegen = 0, float _evasion = 0, ESkills _skill = 0)
    {
        Slot = slot;
        SpriteCategoryLabel = spriteCategoryLabel;
        Stats = new float[7];
        Name = _name;
        SellPrice = _sellPrice;
        Skill = _skill;
        Stats[(int)EStats.Damage] = _damage;
        Stats[(int)EStats.Armor] = _armor;
        Stats[(int)EStats.MoveSpeed] = _moveSpeed;
        Stats[(int)EStats.Health] = _health;
        Stats[(int)EStats.HealthRegen] = _healthRegen;
        Stats[(int)EStats.Evasion] = _evasion;
        Stats[(int)EStats.AttackRange] = _attackRange;
    }

    // Parameterless constructor for XML serialization
    private EquippableItem()
    {

    }
}