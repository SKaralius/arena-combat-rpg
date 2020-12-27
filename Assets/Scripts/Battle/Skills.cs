using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Battle
{

    public class Skills : MonoBehaviour
    {
        public Dictionary<ESkills, Skill> skillsList = new Dictionary<ESkills, Skill>();
        public BattleSystem battleSystem;
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

            #region Register Skills
            skillsList[ESkills.HitTwice] = new Skill(effect: HitTwice, name: "Double Hit", skillRange: 0, skillType: ESkillType.Offensive);
            skillsList[ESkills.Knockback] = new Skill(effect: Knockback, name: "Knockback", skillRange: 0, skillType: ESkillType.Offensive);
            skillsList[ESkills.MoveBackwards] = new Skill(effect: MoveBackwards, name: "Back", skillRange: 999, skillType: ESkillType.Stall);
            skillsList[ESkills.MoveForwards] = new Skill(effect: MoveForwards, name: "Forwards", skillRange: 999, skillType: ESkillType.Offensive);
            skillsList[ESkills.BasicAttack] = new Skill(effect: BasicAttack, name: "Attack", skillRange: 0, skillType: ESkillType.Offensive);
            skillsList[ESkills.DamageOverTime] = new Skill(effect: DamageOverTime, name: "Damage Over Time", skillRange: 0, skillType: ESkillType.Offensive);
            skillsList[ESkills.BuffEvasion] = new Skill(effect: BuffEvasion, name: "Evade", skillRange: 999, skillType: ESkillType.Defensive);
            #endregion Register Skills
        }

        #region Movement
        public IEnumerator MoveBackwards(Controller current, Controller opponent)
        {
            current.Animator.SetTrigger("Walk");
            float positionX = current.transform.position.x + (current.UnitStats.GetStat(EStats.MoveSpeed) * (int)Mathf.Sign(current.transform.localScale.x) * -1);
            positionX = battleSystem.ConstrainXMovement(positionX);
            yield return StartCoroutine(current.UnitMovement.MoveUnit(positionX, current.AnimationDurations.WalkTime));
        }

        public IEnumerator MoveForwards(Controller current, Controller opponent)
        {
            float margin = 5f;
            current.Animator.SetTrigger("Walk");
            float maximumMovementAndDirection = (current.UnitStats.GetStat(EStats.MoveSpeed) * (int)Mathf.Sign(current.transform.localScale.x));
            float distanceBetweenCharacters = Mathf.Abs(current.transform.position.x - opponent.transform.position.x);
            float finalPositionX;
            float viableMovement = distanceBetweenCharacters - margin;
            if (Mathf.Abs(maximumMovementAndDirection) > viableMovement)
            {
                float opponentSlowValue;
                if (distanceBetweenCharacters > margin)
                {
                    finalPositionX = current.transform.position.x + viableMovement * (int)Mathf.Sign(current.transform.localScale.x);
                    opponentSlowValue = current.UnitStats.GetStat(EStats.MoveSpeed) - viableMovement;
                }
                else
                {
                    finalPositionX = current.transform.position.x;
                    opponentSlowValue = current.UnitStats.GetStat(EStats.MoveSpeed);
                }
                opponent.CharacterActiveEffects.AddEffect(new StatChangeEffect(3, EStats.MoveSpeed, -opponentSlowValue));
                MessageSystem.Print("Opponent slowed");
            }
            else
            {
                finalPositionX = current.transform.position.x + maximumMovementAndDirection;
            }
            finalPositionX = battleSystem.ConstrainXMovement(finalPositionX);
            yield return StartCoroutine(current.UnitMovement.MoveUnit(finalPositionX, current.AnimationDurations.WalkTime));
        }
        #endregion Movement
        #region Offensive
        public IEnumerator BasicAttack(Controller current, Controller opponent)
        {
            current.Animator.SetTrigger("Slash");
            if (!EvadeCheck(opponent))
            {
                GetAttacked(current, opponent);
            }
            yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
        }
        public IEnumerator HitTwice(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.HitTwice, 2);
            yield return StartCoroutine(BasicAttack(current, opponent));
            yield return StartCoroutine(BasicAttack(current, opponent));
        }

        public IEnumerator Knockback(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.Knockback, 0);

            current.Animator.SetTrigger("Slash");
            bool evaded = EvadeCheck(opponent);
            if (!evaded)
                GetAttacked(current, opponent, disableAnimation: false);
            int direction = (int)Mathf.Sign(opponent.transform.localScale.x) * -1;
            float distanceMultiplier = 5f;
            if (!evaded)
            {
               opponent.Animator.SetTrigger("Knockbacked");
               //yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
               yield return StartCoroutine(opponent.UnitMovement.MoveUnit((direction * distanceMultiplier) + opponent.transform.position.x, current.AnimationDurations.KnockbackedTime));
            } else
            {
                yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
            }
        }
        public IEnumerator DamageOverTime(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.DamageOverTime, 2);
            current.Animator.SetTrigger("Slash");
            if (!EvadeCheck(opponent))
            {
                GetAttacked(current, opponent);
                opponent.CharacterActiveEffects.AddEffect(new CurrentHealthEffect(2, 20));
            }
            yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
        }
        #endregion Offensive
        #region Buffs
        public IEnumerator BuffEvasion(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.BuffEvasion, 2);
            current.CharacterActiveEffects.AddEffect(new StatChangeEffect(2, EStats.Evasion, 80));
            yield break;
        }
        #endregion Buffs
        #region Helper Methods
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
                opponent.Animator.SetTrigger("Evade");
            }
            return evaded;
        }
        private bool CriticalCheck(Controller current)
        {
            bool isCritical = Random.Range(0, 100) < current.UnitStats.GetStat(EStats.Critical);
            if (isCritical)
            {
                MessageSystem.Print("Critical hit!");
            }
            return isCritical;
        }
        private void GetAttacked(Controller current, Controller opponent, bool disableAnimation = false)
        {
            if (CriticalCheck(current))
                opponent.TakeDamage(current.UnitStats.GetStat(EStats.Damage) * 2);
            else
                opponent.TakeDamage(current.UnitStats.GetStat(EStats.Damage));
            if (!disableAnimation)
                opponent.Animator.SetTrigger("Defend");
            current.ParticleSystems.evaded = false;
        }

        #endregion Helper Methods
    } 
}