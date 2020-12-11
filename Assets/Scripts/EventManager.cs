using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ItemEventHandler();

    public static event ItemEventHandler OnItemAddedToShop;

    public static event ItemEventHandler OnItemRemovedFromShop;

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
}