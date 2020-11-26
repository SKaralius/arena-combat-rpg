using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Stats playerStats;
    private void Awake()
    {
        playerStats = GetComponent<Stats>();
    }
    public void MoveUnit(int isRight)
    {
        transform.Translate(new Vector3((playerStats.moveSpeed * isRight), 0, 0 ));
    }
}
