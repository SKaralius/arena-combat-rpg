using Unit;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class GameManager : MonoBehaviour
{
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
}