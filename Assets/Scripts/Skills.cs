using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public enum ESkills
    {
        None,
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
        //skillsList[ESkills.SetOnFire] = SetOnFire;
        //skillsList[ESkills.Jump] = Jump;
    }

    public IEnumerator BasicAttack(BattleSystem battleSystem, float wait, Controller current, Controller opponent)
    {
        yield return new WaitForSeconds(wait);
        opponent.TakeDamage(battleSystem.Player);
        current.GetComponent<Animator>().SetBool("isAttacking", true);
        yield return new WaitForSeconds(wait + 0.3f);
        current.GetComponent<Animator>().SetBool("isAttacking", false);
    }

    public IEnumerator HitTwice(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        StartCoroutine(BasicAttack(battleSystem, 0f, current, opponent));
        yield return StartCoroutine(BasicAttack(battleSystem, 0.6f, current, opponent));
    }

    public IEnumerator Knockback(BattleSystem battleSystem, Controller current, Controller opponent)
    {
        yield return StartCoroutine(BasicAttack(battleSystem, 0f, current, opponent));
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