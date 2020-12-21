using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Dictionary<ESkills, Skill> skillsList = new Dictionary<ESkills, Skill>();
    // Awake

    #region Singleton logic

    public static Skills instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        #endregion Singleton logic

        skillsList[ESkills.HitTwice] = new Skill(_effect: HitTwice, _name: "Double Hit", _isAffectedByRange: true);
        skillsList[ESkills.Knockback] = new Skill(_effect: Knockback, _name: "Knockback", _isAffectedByRange: true);
        skillsList[ESkills.MoveBackwards] = new Skill(_effect: MoveBackwards, _name: "Back");
        skillsList[ESkills.MoveForwards] = new Skill(_effect: MoveForwards, _name: "Forwards");
        skillsList[ESkills.BasicAttack] = new Skill(_effect: BasicAttack, _name: "Attack", _isAffectedByRange: true);
        skillsList[ESkills.DamageOverTime] = new Skill(_effect: DamageOverTime, _name: "Damage Over Time");
        skillsList[ESkills.BuffEvasion] = new Skill(_effect: BuffEvasion, _name: "Evade");
    }


    public IEnumerator BasicAttack(Controller current, Controller opponent)
    {
        if(!EvadeCheck(opponent))
            opponent.TakeDamage(current.UnitStats.GetStat(EStats.Damage));
        current.Animator.SetTrigger("Slash");
        yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
    }

    public IEnumerator MoveBackwards(Controller current, Controller opponent)
    {
        current.Animator.SetTrigger("Walk");
        float positionX = current.transform.position.x + (current.UnitStats.GetStat(EStats.MoveSpeed) * (int)Mathf.Sign(current.transform.localScale.x) * -1);
        yield return StartCoroutine(current.UnitMovement.MoveUnit(positionX, current.AnimationDurations.WalkTime));
    }

    public IEnumerator MoveForwards(Controller current, Controller opponent)
    {
        float margin = 5f;
        current.Animator.SetTrigger("Walk");
        float moveDistanceAndDirection = (current.UnitStats.GetStat(EStats.MoveSpeed) * (int)Mathf.Sign(current.transform.localScale.x));
        float distanceToOpponent = Mathf.Abs(current.transform.position.x - opponent.transform.position.x);
        float finalPositionX;
        if ((distanceToOpponent - margin) < Mathf.Abs(moveDistanceAndDirection))
        {
            if (distanceToOpponent > margin)
                finalPositionX = current.transform.position.x + (distanceToOpponent - margin) * (int)Mathf.Sign(current.transform.localScale.x);
            else
                finalPositionX = current.transform.position.x;
        }
        else
        {
            finalPositionX = current.transform.position.x + moveDistanceAndDirection;
        }
        yield return StartCoroutine(current.UnitMovement.MoveUnit(finalPositionX, current.AnimationDurations.WalkTime));
    }

    public IEnumerator HitTwice(Controller current, Controller opponent)
    {
        AddSkillCooldown(current,ESkills.HitTwice, 2);
        yield return StartCoroutine(BasicAttack(current, opponent));
        yield return StartCoroutine(BasicAttack(current, opponent));
    }

    public IEnumerator Knockback(Controller current, Controller opponent)
    {
        AddSkillCooldown(current, ESkills.Knockback, 4);
        bool evaded = EvadeCheck(opponent);

        if(!evaded)
            opponent.TakeDamage(current.UnitStats.GetStat(EStats.Damage));
        current.Animator.SetTrigger("Slash");
        yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
        int direction = (int)Mathf.Sign(opponent.transform.localScale.x) * -1;
        float distanceMultiplier = 5f;
        if (!evaded)
            yield return StartCoroutine(opponent.UnitMovement.MoveUnit((direction * distanceMultiplier) + opponent.transform.position.x, current.AnimationDurations.WalkTime));
    }
    public IEnumerator DamageOverTime(Controller current, Controller opponent)
    {
        AddSkillCooldown(current, ESkills.DamageOverTime, 2);
        bool evaded = EvadeCheck(opponent);

        if (!evaded)
        {
            opponent.TakeDamage(current.UnitStats.GetStat(EStats.Damage));
            opponent.CharacterActiveEffects.AddEffect(new CurrentHealthEffect(2, 20));
        }
        current.Animator.SetTrigger("Slash");
        yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
    }
    public IEnumerator BuffEvasion(Controller current, Controller opponent)
    {
        AddSkillCooldown(current,ESkills.BuffEvasion , 2);
        current.CharacterActiveEffects.AddEffect(new StatChangeEffect(2, EStats.Evasion, 80));
        yield break;
    }

    private static void AddSkillCooldown(Controller current, ESkills skill, int cooldownLength)
    {
        current.characterCooldowns.AddCooldownToSkill(skill, cooldownLength);
        SkillManager skillManager = current.GetComponent<SkillManager>();
        if (skillManager != null)
            skillManager.RenderSkillCooldowns();
    }

    private bool EvadeCheck(Controller opponent)
    {
        bool evaded = Random.Range(0, 100) < opponent.UnitStats.GetStat(EStats.Evasion);
        if (evaded)
        {
            MessageSystem.Print("Attack was evaded");
        }
        return evaded;
    }
}