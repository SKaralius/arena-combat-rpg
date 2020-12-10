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
        skillsList[ESkills.BasicAttack] = new Skill(_effect: (BattleSystem battleSystem, Controller current, Controller opponent) => BasicAttack(battleSystem, current, opponent), _name: "Attack", _isAffectedByRange: true);
    }

    public IEnumerator BasicAttack(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        if(!EvadeCheck(opponent))
            opponent.TakeDamage(opponent);
        current.GetComponent<Animator>().SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        current.GetComponent<Animator>().SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator MoveBackwards(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        current.GetComponent<Animator>().SetBool("isWalking", true);
        yield return StartCoroutine(current.GetComponent<UnitMovement>().MoveUnit((int)Mathf.Sign(current.transform.localScale.x) * -1));
        current.GetComponent<Animator>().SetBool("isWalking", false);
    }

    public IEnumerator MoveForwards(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        current.GetComponent<Animator>().SetBool("isWalking", true);
        yield return StartCoroutine(current.GetComponent<UnitMovement>().MoveUnit((int)Mathf.Sign(current.transform.localScale.x)));
        current.GetComponent<Animator>().SetBool("isWalking", false);
    }

    public IEnumerator HitTwice(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        current.characterCooldowns.AddCooldownToSkill(ESkills.HitTwice, 2);
        SkillManager.instance.RenderSkillCooldowns();
        yield return StartCoroutine(BasicAttack(battleSystem, current, opponent));
        yield return StartCoroutine(BasicAttack(battleSystem, current, opponent));
    }

    public IEnumerator Knockback(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        current.characterCooldowns.AddCooldownToSkill(ESkills.Knockback, 5);
        SkillManager.instance.RenderSkillCooldowns();
        bool evaded = EvadeCheck(opponent);

        if(!evaded)
            opponent.TakeDamage(opponent);
        current.GetComponent<Animator>().SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        if(!evaded)
            yield return StartCoroutine(current.GetComponent<UnitMovement>().MoveUnit((int)Mathf.Sign(current.transform.localScale.x) * -1));
        current.GetComponent<Animator>().SetBool("isAttacking", false);
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

    //public void SetOnFire()
    //{
    //    MessageSystem.Print("On Fire");
    //}
    //public void Jump()
    //{
    //    MessageSystem.Print("Jump");
    //}
}