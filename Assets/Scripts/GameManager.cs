using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameManager : MonoBehaviour
{
    private BattleState state;
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
        BattleStateChange(BattleState.START);
        // Opponent introduction
        // Start a coroutine to give some time for the player to take in what's happening
        // Change state to player turn in the coroutine and call playerturn function
        BattleStateChange(BattleState.PLAYERTURN);
    }

    void PlayerTurn()
    {
        // Display skills, or make it clear that player can make a choice now otherwise
        Debug.Log("Hello world");
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(0.5f);
        float remainingHealth = playerUnit.TakeDamage(25f);
        if (remainingHealth <= 0)
        {
            BattleStateChange(BattleState.LOST);
        }
        else
        {
            BattleStateChange(BattleState.PLAYERTURN);
        }
    }

    IEnumerator PlayerAttack()
    {
        BattleStateChange(BattleState.ENEMYTURN);
        float remainingHealth = opponentUnit.TakeDamage(player.GetComponent<UnitStats>().GetDamage());
        yield return new WaitForSeconds(1f);
        if (remainingHealth <= 0)
        {
            BattleStateChange(BattleState.WON);
        } else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        else
        {
            StartCoroutine(PlayerAttack());
        }
    }
    public void BattleStateChange(BattleState bs)
    {
        state = bs;
        EventManager.BattleStateChanged(bs);
    }
}
