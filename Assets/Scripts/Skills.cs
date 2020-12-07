using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    #region Singleton logic
    public static Skills instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion
    public void HitTwice()
    {
        MessageSystem.Print("Hit Twice");
    }
    public void SetOnFire()
    {
        MessageSystem.Print("On Fire");
    }
    public void Jump()
    {
        MessageSystem.Print("Jump");
    }
}
