using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ItemEventHandler();

    public static event ItemEventHandler OnItemAddedToShop;

    public static event ItemEventHandler OnItemRemovedFromShop;

    public delegate void ItemEquipHandler(EquippableItem item, int who);

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);

    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void GoldHandler(int amountToAdd);

    public static event GoldHandler OnGoldChange;

    //public delegate void StatsEventHandler();
    //public static event StatsEventHandler OnStatsChanged;

    //public delegate void BattleStateEventHandler(BattleState state);
    //public static event BattleStateEventHandler OnBattleStateChange;

    //public delegate void PlayerLevelEventHandler();
    //public static event PlayerLevelEventHandler OnPlayerLevelChange;

    public static void ItemAddedToShop()
    {
        OnItemAddedToShop?.Invoke();
    }

    public static void ItemRemovedFromShop()
    {
        OnItemRemovedFromShop?.Invoke();
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public static void GoldChanged(int amountToAdd)
    {
        OnGoldChange?.Invoke(amountToAdd);
    }
}