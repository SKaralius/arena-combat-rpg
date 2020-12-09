using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public enum ESkills
    {
        None,
        BasicAttack,
        MoveForwards,
        MoveBackwards,
        HitTwice,
        SetOnFire,
        Jump,
        Knockback
    }

    public Dictionary<ESkills, Skill> skillsList = new Dictionary<ESkills, Skill>();

    #region Singleton logic

    public static Skills instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion Singleton logic

    private void Start()
    {
        skillsList[ESkills.HitTwice] = new Skill(_effect: HitTwice, _isAffectedByRange: true);
        skillsList[ESkills.Knockback] = new Skill(_effect: Knockback, _isAffectedByRange: true);
        skillsList[ESkills.MoveBackwards] = new Skill(_effect: MoveBackwards);
        skillsList[ESkills.MoveForwards] = new Skill(_effect: MoveForwards);
        skillsList[ESkills.BasicAttack] = new Skill(_effect: (BattleSystem battleSystem, Controller current, Controller opponent) => BasicAttack(battleSystem, current, opponent, wait: 0), _isAffectedByRange: true);
        //skillsList[ESkills.SetOnFire] = SetOnFire;
        //skillsList[ESkills.Jump] = Jump;
    }

    public IEnumerator BasicAttack(BattleSystem battleSystem, Controller current, Controller opponent, float wait)
    {
        yield return new WaitForSeconds(wait);
        opponent.TakeDamage(battleSystem.Player);
        current.GetComponent<Animator>().SetBool("isAttacking", true);
        yield return new WaitForSeconds(wait + 0.3f);
        current.GetComponent<Animator>().SetBool("isAttacking", false);
    }

    public IEnumerator MoveBackwards(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        current.GetComponent<Animator>().SetBool("isWalking", true);
        current.MoveUnit((int)Mathf.Sign(current.transform.localScale.x) * -1);
        yield return new WaitForSeconds(1f);
        current.GetComponent<Animator>().SetBool("isWalking", false);
    }    
    public IEnumerator MoveForwards(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        current.GetComponent<Animator>().SetBool("isWalking", true);
        current.MoveUnit((int)Mathf.Sign(current.transform.localScale.x));
        yield return new WaitForSeconds(1f);
        current.GetComponent<Animator>().SetBool("isWalking", false);
    }

    public IEnumerator HitTwice(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        StartCoroutine(BasicAttack(battleSystem, current, opponent, 0f));
        yield return StartCoroutine(BasicAttack(battleSystem, current, opponent, 0.6f));
    }

    public IEnumerator Knockback(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        yield return StartCoroutine(BasicAttack(battleSystem, current, opponent, 0f));
        opponent.MoveUnit((int)Mathf.Sign(opponent.transform.localScale.x) * -1);
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