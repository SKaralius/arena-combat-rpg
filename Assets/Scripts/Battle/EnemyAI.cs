using System;
using System.Collections;
using Unit;
using UnityEngine;
using System.Collections.Generic;

namespace Battle
{

    public class EnemyAI : MonoBehaviour
    {
        private List<ESkills> enemySkills;
        private bool reachedEdge = false;
        private int runCounter = 0;
        private void Awake()
        {
            enemySkills = GetComponent<CharacterSkills>().characterSkills;
        }
        public UseSkillHandler DecideOrder(BattleSystem battleSystem)
        {
            List<ESkills>[] priorityExecutionList = new List<ESkills>[3];

            // Initialize the array of lists
            for (int i = 0; i < priorityExecutionList.Length; i++)
            {
                priorityExecutionList[i] = new List<ESkills>();
            }
            // Decide priority
            foreach (ESkills skill in enemySkills)
            {
                bool distanceCheck = battleSystem.GetDistanceBetweenFighters() < Skills.instance.skillsList[skill].SkillRange + battleSystem.Enemy.UnitStats.GetStat(EStats.AttackRange);
                if (distanceCheck && battleSystem.Enemy.characterCooldowns.cooldowns[skill] == 0)
                {
                    if (Skills.instance.skillsList[skill].SkillType == ESkillType.Offensive)
                    {
                        priorityExecutionList[0].Add(skill);
                    } else if (Skills.instance.skillsList[skill].SkillType == ESkillType.Defensive)
                    {
                        priorityExecutionList[1].Add(skill);
                    } else if (Skills.instance.skillsList[skill].SkillType == ESkillType.Stall)
                    {
                        priorityExecutionList[2].Add(skill);
                    }
                }
            }
            // Remove basic skills if possible
            if (priorityExecutionList[0].Count > 1)
            {
                priorityExecutionList[0].Remove(ESkills.MoveForwards);
            }            
            if (priorityExecutionList[0].Count > 1)
            {
                priorityExecutionList[0].Remove(ESkills.BasicAttack);
            }

            // If is low on health and losing, use defensive or stalling skills
            bool isLowerHealth = battleSystem.Enemy.Health < battleSystem.Player.Health;
            bool isLowOnHealth = battleSystem.Enemy.Health < battleSystem.Enemy.UnitStats.GetStat(EStats.Health) * 0.2f;
            float potentialX = battleSystem.Enemy.transform.position.x + battleSystem.Enemy.UnitStats.GetStat(EStats.MoveSpeed);
            bool isAtEdge = potentialX >= battleSystem.ConstrainXMovement(500f);

            if (isLowOnHealth && isLowerHealth && !isAtEdge && !reachedEdge && runCounter < 4)
            {
                priorityExecutionList[0].Clear();
                runCounter++;
            }
            if (isAtEdge)
            {
                reachedEdge = true;
            }
            float chanceToStall = UnityEngine.Random.Range(0, 100);
            if (chanceToStall < 20)
            {
                priorityExecutionList[0].Clear();
            }

            // Return a skill based on priority.
            foreach (List<ESkills> priorityList in priorityExecutionList)
            {
                if (priorityList.Count > 0)
                {
                    int roll = UnityEngine.Random.Range(0, priorityList.Count);
                    return Skills.instance.skillsList[priorityList[roll]].Effect;
                }
            }
            return Skills.instance.skillsList[ESkills.MoveForwards].Effect;
        }
    } 
}