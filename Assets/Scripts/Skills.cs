using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;

public class Skills : MonoBehaviour
{
    public enum ESkills
    {
        None,
        HitTwice,
        SetOnFire,
        Jump
    }
    public delegate IEnumerator UseSkillHandler(BattleSystem battleSystem,Controller current, Controller opponent);
    public Dictionary<ESkills, UseSkillHandler> skillsList = new Dictionary<ESkills, UseSkillHandler>();
    #region Singleton logic
    public static Skills instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion
    private void Start()
    {
        skillsList[ESkills.HitTwice] = HitTwice;
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
    //public void SetOnFire()
    //{
    //    MessageSystem.Print("On Fire");
    //}
    //public void Jump()
    //{
    //    MessageSystem.Print("Jump");
    //}
}
