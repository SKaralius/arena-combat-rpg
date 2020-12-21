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

    public IEnumerator BasicAttack(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        Animator currentAnimator = current.GetComponentInChildren<Animator>();
        if(!EvadeCheck(opponent))
            opponent.TakeDamage(current.GetComponent<UnitStats>().GetStat(EStats.Damage));
        currentAnimator.SetTrigger("Slash");
        yield return new WaitForSeconds(current.GetComponent<AnimationDurations>().SlashTime);
    }

    public IEnumerator MoveBackwards(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        Animator currentAnimator = current.GetComponentInChildren<Animator>();
        currentAnimator.SetTrigger("Walk");
        float positionX = current.transform.position.x + (current.GetComponent<UnitStats>().GetStat(EStats.MoveSpeed) * (int)Mathf.Sign(current.transform.localScale.x) * -1);
        yield return StartCoroutine(current.GetComponent<UnitMovement>().MoveUnit(positionX));
    }

    public IEnumerator MoveForwards(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        Animator currentAnimator = current.GetComponentInChildren<Animator>();
        float margin = 5f;
        currentAnimator.SetTrigger("Walk");
        float moveDistanceAndDirection = (current.GetComponent<UnitStats>().GetStat(EStats.MoveSpeed) * (int)Mathf.Sign(current.transform.localScale.x));
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
        yield return StartCoroutine(current.GetComponent<UnitMovement>().MoveUnit(finalPositionX));
    }

    public IEnumerator HitTwice(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        AddSkillCooldown(current, 2);
        yield return StartCoroutine(BasicAttack(battleSystem, current, opponent));
        yield return StartCoroutine(BasicAttack(battleSystem, current, opponent));
    }

    public IEnumerator Knockback(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        Animator currentAnimator = current.GetComponentInChildren<Animator>();
        AddSkillCooldown(current, 4);
        bool evaded = EvadeCheck(opponent);

        if(!evaded)
            opponent.TakeDamage(current.GetComponent<UnitStats>().GetStat(EStats.Damage));
        currentAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        currentAnimator.SetBool("isAttacking", false);
        if(!evaded)
            yield return StartCoroutine(opponent.GetComponent<UnitMovement>().MoveUnit(((int)Mathf.Sign(opponent.transform.localScale.x) * -1 * 5f) + opponent.transform.position.x));
    }
    public IEnumerator DamageOverTime(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        Animator currentAnimator = current.GetComponentInChildren<Animator>();
        AddSkillCooldown(current, 2);
        bool evaded = EvadeCheck(opponent);

        if (!evaded)
        {
            opponent.TakeDamage(current.GetComponent<UnitStats>().GetStat(EStats.Damage));
            opponent.GetComponent<CharacterActiveEffects>().AddEffect(new CurrentHealthEffect(2, 20));
        }
        currentAnimator.SetBool("isAttacking", true);
        float animationLength = currentAnimator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(animationLength);
        currentAnimator.SetBool("isAttacking", false);
    }
    public IEnumerator BuffEvasion(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        AddSkillCooldown(current, 2);
        current.GetComponent<CharacterActiveEffects>().AddEffect(new StatChangeEffect(2, EStats.Evasion, 80));
        yield break;
    }

    private static void AddSkillCooldown(Controller current, int cooldownLength)
    {
        current.characterCooldowns.AddCooldownToSkill(ESkills.BuffEvasion, cooldownLength);
        SkillManager skillManager = current.GetComponent<SkillManager>();
        if (skillManager != null)
            skillManager.RenderSkillCooldowns();
    }

    private bool EvadeCheck(Controller opponent)
    {
        bool evaded = Random.Range(0, 100) < opponent.GetComponent<UnitStats>().GetStat(EStats.Evasion);
        if (evaded)
        {
            MessageSystem.Print("Attack was evaded");
        }
        return evaded;
    }
}