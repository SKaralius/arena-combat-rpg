using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private Unit playerUnit;
    private GameObject opponent;
    private Unit opponentUnit;
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
        playerUnit = player.GetComponent<Unit>();

        opponent = GameObject.FindWithTag("Enemy");
        opponentUnit = opponent.GetComponent<Unit>();

        opponent.GetComponent<EquippedItems>().Equip(new EquipableItem(EquipSlot.LeftWeapon, "Left Sword", ("Weapon", "Sword"), _sellPrice: 69, _damage: 5, _evasion: 30));
        //List<string> spriteNames = new List<string>(itemSpriteLibrary.GetCategoryNames());
        //spriteNames.ForEach((name) => Debug.Log(name));
        //Sprite staffSprite = itemSpriteLibrary.GetSprite("Weapon", "Staff");
    }
}
