using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnFSM;
using Unit;

public class BattleSystem : StateMachine
{
    public Controller Player;
    public Controller Enemy;

    // Start is called before the first frame update
    void Start()
    {
        SetState(new Begin(this));
    }

    public void OnAttackButton()
    {
        StartCoroutine(State.Attack());
    }
    public void OnMoveButton(int i)
    {
        StartCoroutine(State.Move(i));
    }
    public void OnItemEquipButton(IEquipable equipable)
    {
        StartCoroutine(State.Equip(equipable));
    }
}