using System;
using System.Collections;
using Unit;
public enum AIOrders
{
    MoveLeft,
    MoveRight,
    Attack,
    UseSkill
}

public static class EnemyAI
{
    public static Skill.UseSkillHandler DecideOrder(BattleSystem battleSystem)
    {
            return Skills.instance.skillsList[battleSystem.Enemy.GetComponent<CharacterSkills>().characterSkills[0]].effect;
    }
}