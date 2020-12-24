using Unit;

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

    public EquippableItem(EquipSlot slot, string _name, (string, string) spriteCategoryLabel, int _sellPrice, float _attackRange = 0, float _damage = 0, float _armor = 0, float _moveSpeed = 0, float _health = 0, float _evasion = 0, ESkills _skill = ESkills.None)
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
        Stats[(int)EStats.Evasion] = _evasion;
        Stats[(int)EStats.AttackRange] = _attackRange;
    }

    public float GetItemScore()
    {
        float itemScore = 0;
        itemScore += Stats[(int)EStats.Evasion] * 3;
        itemScore += Stats[(int)EStats.Armor] * 3;
        itemScore += Stats[(int)EStats.Health];
        itemScore += Stats[(int)EStats.Damage];
        itemScore += Stats[(int)EStats.MoveSpeed];
        itemScore += Stats[(int)EStats.AttackRange];
        return itemScore;
    }

    // Parameterless constructor for XML serialization
    private EquippableItem()
    {

    }
}