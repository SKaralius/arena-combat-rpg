﻿using TurnFSM;
using Unit;
using UnityEngine;

public class BattleSystem : StateMachine
{
    public Controller Player;
    public Controller Enemy;

    // Start is called before the first frame update
    private void Start()
    {
        SetState(new Begin(this));
        EventManager.OnItemEquipped += OnItemEquipButton;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
    }

    public void OnAttackButton()
    {
        if (IsOpponentWithinAttackRange(Player))
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

    public float GetDistanceBetweenFighters()
    {
        return Mathf.Abs(Player.gameObject.transform.position.x - Enemy.gameObject.transform.position.x);
    }

    public bool IsOpponentWithinAttackRange(Controller current)
    {
        float distance = GetDistanceBetweenFighters();
        return distance < current.GetComponent<UnitStats>().GetStat(EStats.AttackRange);
    }

    private void OnDestroy()
    {
        EventManager.OnItemEquipped -= OnItemEquipButton;
    }
}