using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ItemEventHandler();

    public static event ItemEventHandler OnItemAddedToInventory;

    public static event ItemEventHandler OnItemRemovedFromInventory;

    public static event ItemEventHandler OnItemAddedToShop;

    public static event ItemEventHandler OnItemRemovedFromShop;

    public delegate void ItemEquipHandler(EquippableItem item, int who);

    public static event ItemEquipHandler OnItemEquipped;

    public static event ItemEquipHandler OnItemUnequipped;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);

    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void GoldHandler(int amountToAdd);

    public static event GoldHandler OnGoldChange;

    public delegate void ShopHandler();

    public static event ShopHandler OnShopToggle;

    //public delegate void StatsEventHandler();
    //public static event StatsEventHandler OnStatsChanged;

    //public delegate void BattleStateEventHandler(BattleState state);
    //public static event BattleStateEventHandler OnBattleStateChange;

    //public delegate void PlayerLevelEventHandler();
    //public static event PlayerLevelEventHandler OnPlayerLevelChange;

    public static void ShopToggled()
    {
        OnShopToggle?.Invoke();
    }

    public static void ItemAddedToInventory()
    {
        OnItemAddedToInventory?.Invoke();
    }

    public static void ItemRemovedFromInventory()
    {
        OnItemRemovedFromInventory?.Invoke();
    }

    public static void ItemAddedToShop()
    {
        OnItemAddedToShop?.Invoke();
    }

    public static void ItemRemovedFromShop()
    {
        OnItemRemovedFromShop?.Invoke();
    }

    public static void ItemEquipped(EquippableItem item, int who)
    {
        OnItemEquipped?.Invoke(item, who);
    }

    public static void ItemUnequipped(EquippableItem item, int who)
    {
        OnItemUnequipped?.Invoke(item, who);
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public static void GoldChanged(int amountToAdd)
    {
        OnGoldChange?.Invoke(amountToAdd);
    }

    //public static void BattleStateChanged(BattleState state)
    //{
    //    OnBattleStateChange?.Invoke(state);
    //}

    //public static void StatsChanged()
    //{
    //    if (OnStatsChanged != null)
    //        OnStatsChanged();
    //}

    //public static void PlayerLevelChanged()
    //{
    //    if (OnPlayerLevelChange != null)
    //        OnPlayerLevelChange();
    //}
}