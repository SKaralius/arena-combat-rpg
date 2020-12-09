using System;
using System.Collections;
using Unit;
using UnityEngine;
using System.Collections.Generic;
public enum AIOrders
{
    MoveLeft,
    MoveRight,
    Attack,
    UseSkill
}

public class EnemyAI : MonoBehaviour
{
    private List<ESkills> enemySkills;
    private void Start()
    {
        enemySkills = GetComponent<CharacterSkills>().characterSkills;
    }
    public Skill.UseSkillHandler DecideOrder(BattleSystem battleSystem)
    {
        List<ESkills> distanceSensitiveSkills = new List<ESkills>();
        foreach (ESkills skill in enemySkills)
        {
            if (Skills.instance.skillsList[skill].IsAffectedByRange && battleSystem.Enemy.characterCooldowns.cooldowns[skill] == 0)
            {
                distanceSensitiveSkills.Add(skill);
            }
        }
        if (battleSystem.IsOpponentWithinAttackRange(battleSystem.Enemy))
        {
            return Skills.instance.skillsList[distanceSensitiveSkills[UnityEngine.Random.Range(0, distanceSensitiveSkills.Count)]].Effect;
        }
        else
        {
            return Skills.instance.skillsList[enemySkills[2]].Effect;
        }
    }
}