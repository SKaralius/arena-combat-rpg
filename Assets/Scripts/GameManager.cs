using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using Unit;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private Controller playerUnit;
    private GameObject opponent;
    private Controller opponentUnit;
    public SpriteLibraryAsset itemSpriteLibrary;

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
        player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = player.GetComponent<Controller>();

        opponent = GameObject.FindWithTag("Enemy");
        opponentUnit = opponent.GetComponent<Controller>();

        //List<string> spriteNames = new List<string>(itemSpriteLibrary.GetCategoryNames());
        //spriteNames.ForEach((name) => Debug.Log(name));
        //Sprite staffSprite = itemSpriteLibrary.GetSprite("Weapon", "Staff");
    }
}
