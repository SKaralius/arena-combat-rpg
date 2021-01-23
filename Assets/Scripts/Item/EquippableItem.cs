using Unit;
using System.Collections.Generic;

[System.Serializable]
public class EquippableItem : IItem, IEquipable
{
    // IItem
    public string Name { get; set; }

    public (string, string) SpriteCategoryLabel { get; set;  }
    public int SellPrice { get; set; }
    public ESkills Skill;

    // IEquipable
    public EquipSlot Slot { get; set; }

    // Stats

    public Stats Stats { get; set; }

    public EquippableItem(EquipSlot slot, string _name, (string, string) spriteCategoryLabel,
        int _sellPrice, Stats _stats,
        ESkills _skill = ESkills.None)
    {
        Slot = slot;
        SpriteCategoryLabel = spriteCategoryLabel;
        Name = _name;
        SellPrice = _sellPrice;
        Skill = _skill;
        Stats = _stats;
    }

    // Parameterless constructor for XML serialization
    private EquippableItem()
    {

    }
}