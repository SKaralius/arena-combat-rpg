    public interface IEquipable
    {
        EquipSlot Slot { get; }
        (string, string) SpriteCategoryLabel { get; }
    }