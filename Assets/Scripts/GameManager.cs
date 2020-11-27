using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Unit playerUnit;
    private GameObject opponent;
    private Unit opponentUnit;

    public static GameManager instance;
    #region Singleton logic
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        player = GameObject.Find("PlayerUnit");
        playerUnit = player.GetComponent<Unit>();

        opponent = GameObject.Find("Unit");
        opponentUnit = opponent.GetComponent<Unit>();

        opponent.GetComponent<EquippedItems>().Equip(new EquipableItem(EquipSlot.Head, "NPC armor", 54f, _armor: 100));
        opponent.GetComponent<EquippedItems>().Equip(new EquipableItem(EquipSlot.Chest, "NPC armor", 54f, _health: 100));
    }

    void PlayerTurn()
    {
        // Display skills, or make it clear that player can make a choice now otherwise
        Debug.Log("Hello world");
    }
}
