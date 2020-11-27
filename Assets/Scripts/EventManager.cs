using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void ItemEventHandler();
    public static event ItemEventHandler OnItemAddedToInventory;
    public static event ItemEventHandler OnItemRemovedFromInventory;

    public delegate void ItemEquipHandler(IItem item);
    public static event ItemEquipHandler OnItemEquipped;
    public static event ItemEquipHandler OnItemUnequipped;

    public delegate void PlayerHealthEventHandler(int currentHealth, int maxHealth);
    public static event PlayerHealthEventHandler OnPlayerHealthChanged;

    public delegate void StatsEventHandler();
    public static event StatsEventHandler OnStatsChanged;

    public delegate void BattleStateEventHandler(BattleState state);
    public static event BattleStateEventHandler OnBattleStateChange;

    //public delegate void PlayerLevelEventHandler();
    //public static event PlayerLevelEventHandler OnPlayerLevelChange;

    public static void ItemAddedToInventory()
    {
        OnItemAddedToInventory?.Invoke();
    }

    public static void ItemRemovedFromInventory()
    {
        OnItemRemovedFromInventory?.Invoke();
    }

    //public static void ItemAddedToInventory(List<Item> items)
    //{
    //    if (OnItemAddedToInventory != null)
    //    {
    //        foreach (Item item in items)
    //        {
    //            OnItemAddedToInventory(item);
    //        }
    //    }
    //}

    public static void ItemEquipped(IItem item)
    {
        OnItemEquipped?.Invoke(item);
    }
    public static void ItemUnequipped(IItem item)
    {
        OnItemUnequipped?.Invoke(item);
    }

    public static void HealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public static void BattleStateChanged(BattleState state)
    {
        OnBattleStateChange?.Invoke(state);
    }

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
