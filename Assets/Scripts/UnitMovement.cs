using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private UnitStats playerStats;
    private void Awake()
    {
        playerStats = GetComponent<UnitStats>();
    }
    public void MoveUnit(int isRight)
    {
        transform.Translate(new Vector3((playerStats.GetMoveSpeed() * isRight), 0, 0 ));
    }
}
