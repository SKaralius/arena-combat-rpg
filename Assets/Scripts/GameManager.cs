using Unit;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private Controller playerUnit;
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

    #endregion Singleton logic

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerUnit = player.GetComponent<Controller>();
    }
}