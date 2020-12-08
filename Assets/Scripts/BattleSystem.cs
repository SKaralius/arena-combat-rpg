using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnFSM;
using Unit;

public class BattleSystem : StateMachine
{
    public Controller Player;
    public Controller Enemy;
    public bool enemyInPlayersAttackRange = false;
    public bool playerInEnemysAttackRange = false;

    // Start is called before the first frame update
    void Start()
    {
        SetState(new Begin(this));
        EventManager.OnItemEquipped += OnItemEquipButton;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
    }

    public void OnAttackButton()
    {
        if (enemyInPlayersAttackRange)
        { 
            StartCoroutine(State.Attack());
        }
        else
        {
            MessageSystem.Print("Out of range");
        }
    }
    public void OnMoveButton(int i)
    {
        StartCoroutine(State.Move(i));
    }
    public void OnSkillButton(Skill.UseSkillHandler skill)
    {
        StartCoroutine(State.UseSkill(skill));
    }
    public void OnItemEquipButton(EquippableItem item, int who)
    {
        StartCoroutine(State.Equip(item));
    }
    private void OnDestroy()
    {
        EventManager.OnItemEquipped -= OnItemEquipButton;
    }
}