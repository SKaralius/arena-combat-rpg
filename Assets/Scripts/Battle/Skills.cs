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
        // TODO: remove hard coded spellcast animation sync with defend and evade.
        private readonly float animationDurationUntilCast = 1.05f;

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
            skillsList[ESkills.Execute] = new Skill(effect: Execute, name: "Execute", skillRange: 0, skillType: ESkillType.Offensive);
            skillsList[ESkills.Fireball] = new Skill(effect: Fireball, name: "Fireball", skillRange: 40, skillType: ESkillType.Offensive);
            skillsList[ESkills.Lightning] = new Skill(effect: Lightning, name: "Lightning", skillRange: 60, skillType: ESkillType.Offensive);
            skillsList[ESkills.AirBlast] = new Skill(effect: AirBlast, name: "Air Blast", skillRange: 30, skillType: ESkillType.Defensive);
            skillsList[ESkills.StrongHit] = new Skill(effect: StrongHit, name: "Strong Hit", skillRange: 0, skillType: ESkillType.Offensive);
            skillsList[ESkills.Heal] = new Skill(effect: Heal, name: "Heal", skillRange: 999, skillType: ESkillType.Defensive);
            skillsList[ESkills.HealOverTime] = new Skill(effect: HealOverTime, name: "Heal Over Time", skillRange: 999, skillType: ESkillType.Defensive);
            skillsList[ESkills.EarthStrike] = new Skill(effect: EarthStrike, name: "Earth Strike", skillRange: 30, skillType: ESkillType.Offensive);
            skillsList[ESkills.BuffDamage] = new Skill(effect: BuffDamage, name: "Damage Buff", skillRange: 999, skillType: ESkillType.Offensive); 
            skillsList[ESkills.BuffArmor] = new Skill(effect: BuffArmor, name: "Armor Buff", skillRange: 999, skillType: ESkillType.Defensive); 
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
        public IEnumerator StrongHit(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.StrongHit, 4);
            current.Animator.SetTrigger("Slash");
            if (!EvadeCheck(opponent))
            {
                GetAttacked(current, opponent, skillMultiplier: 1.5f);
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
            {
                GetAttacked(current, opponent, disableAnimation: false);
                yield return new WaitForSeconds(0.1f);
            }
            int direction = (int)Mathf.Sign(opponent.transform.localScale.x) * -1;
            float distanceMultiplier = 5f;
            if (!evaded)
            {
               opponent.Animator.SetTrigger("Knockbacked");
               //yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
               yield return StartCoroutine(opponent.UnitMovement.MoveUnit((direction * distanceMultiplier) + opponent.transform.position.x, current.AnimationDurations.KnockbackedTime));
               yield return new WaitForSeconds(0.3f);
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
                opponent.CharacterActiveEffects.AddEffect(new CurrentHealthEffect(2, -20));
            }
            yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
        }
        public IEnumerator Execute(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.Execute, 9);
            current.Animator.SetTrigger("Slash");
            if (!EvadeCheck(opponent))
            {
                if (opponent.Health < opponent.UnitStats.GetStat(EStats.Health) / 100 * 50)
                {
                    GetAttacked(current, opponent, skillMultiplier: 10);
                    MessageSystem.Print("Executed!");
                }
                else
                    GetAttacked(current, opponent);
            }
            yield return new WaitForSeconds(current.AnimationDurations.SlashTime);
        }        
        public IEnumerator Fireball(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.Fireball, 3);
            current.Animator.SetTrigger("Spellcast");
            yield return new WaitForSeconds(ParticleSystems.projectileTravelDuration + animationDurationUntilCast);
            if (!EvadeCheck(opponent))
            {
                GetAttacked(current, opponent, skillMultiplier: 0.6f);
                opponent.CharacterActiveEffects.AddEffect(new CurrentHealthEffect(2, -5));
            }
            yield return new WaitForSeconds(current.AnimationDurations.EvadeTime);
        }        
        public IEnumerator Lightning(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.Lightning, 5);
            current.ParticleSystems.nextSpellLoad = ParticleSystems.ESpellLoads.Lightning;
            current.ParticleSystems.nextExplosion = ParticleSystems.EExplosions.Lightning;
            current.Animator.SetTrigger("Spellcast");
            yield return new WaitForSeconds(ParticleSystems.projectileTravelDuration + animationDurationUntilCast);
            if (!EvadeCheck(opponent))
            {
                GetAttacked(current, opponent, skillMultiplier: 1.4f);
                opponent.CharacterActiveEffects.AddEffect(new StatChangeEffect(2, EStats.MoveSpeed, -5));
                opponent.CharacterActiveEffects.AddEffect(new StatChangeEffect(2, EStats.Evasion, -5));
            }
            yield return new WaitForSeconds(current.AnimationDurations.EvadeTime);
        }        
        public IEnumerator AirBlast(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.AirBlast, 5);
            current.ParticleSystems.nextSpellLoad = ParticleSystems.ESpellLoads.Air;
            current.ParticleSystems.nextExplosion = ParticleSystems.EExplosions.Air;
            current.Animator.SetTrigger("Spellcast");
            yield return new WaitForSeconds(ParticleSystems.projectileTravelDuration + animationDurationUntilCast);
            if (!EvadeCheck(opponent))
            {
                int direction = (int)Mathf.Sign(opponent.transform.localScale.x) * -1;
                float distanceMultiplier = 30f;
                GetAttacked(current, opponent, skillMultiplier: 0.3f);
                opponent.Animator.SetTrigger("Knockbacked");
                yield return StartCoroutine(opponent.UnitMovement.MoveUnit((direction * distanceMultiplier) + opponent.transform.position.x, current.AnimationDurations.KnockbackedTime));
            }
            yield return new WaitForSeconds(current.AnimationDurations.EvadeTime);
        }                
        public IEnumerator EarthStrike(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.EarthStrike, 8);
            current.ParticleSystems.nextSpellLoad = ParticleSystems.ESpellLoads.Earth;
            current.ParticleSystems.nextExplosion = ParticleSystems.EExplosions.Earth;
            current.Animator.SetTrigger("Spellcast");
            yield return new WaitForSeconds(ParticleSystems.projectileTravelDuration + animationDurationUntilCast);
            if (!EvadeCheck(opponent))
            {
                GetAttacked(current, opponent, skillMultiplier: 0.8f);
                int direction = (int)Mathf.Sign(opponent.transform.localScale.x) * -1;
                float distanceMultiplier = 5f;
                opponent.Animator.SetTrigger("Knockbacked");
                float opponentEvasion = opponent.UnitStats.GetStat(EStats.Evasion);
                float opponentArmor = opponent.UnitStats.GetStat(EStats.Armor);
                opponent.CharacterActiveEffects.AddEffect(new StatChangeEffect(4, EStats.Evasion, -(opponentEvasion / 2)));
                opponent.CharacterActiveEffects.AddEffect(new StatChangeEffect(4, EStats.Armor, -(opponentArmor / 2)));
                yield return StartCoroutine(opponent.UnitMovement.MoveUnit((direction * distanceMultiplier) + opponent.transform.position.x, current.AnimationDurations.KnockbackedTime));
            }
            yield return new WaitForSeconds(current.AnimationDurations.EvadeTime);
        }        
        #endregion Offensive
        #region Buffs
        public IEnumerator BuffEvasion(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.BuffEvasion, 6);
            current.CharacterActiveEffects.AddEffect(new StatChangeEffect(4, EStats.Evasion, 30));
            yield break;
        }        
        public IEnumerator BuffArmor(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.BuffArmor, 6);
            current.CharacterActiveEffects.AddEffect(new StatChangeEffect(4, EStats.Armor, 30));
            yield break;
        }
        public IEnumerator Heal(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.Heal, 5);
            current.Animator.SetTrigger("Spellcast");
            float maxHealth = current.UnitStats.GetStat(EStats.Health);
            if (maxHealth > current.Health)
                current.TakeDamage(-maxHealth * 0.4f);
            current.CharacterActiveEffects.AddEffect(new CurrentHealthEffect(4, 5));
            yield return new WaitForSeconds(current.AnimationDurations.EvadeTime);
        }        
        public IEnumerator HealOverTime(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.HealOverTime, 5);
            current.Animator.SetTrigger("Spellcast");
            float maxHealth = current.UnitStats.GetStat(EStats.Health);
            current.CharacterActiveEffects.AddEffect(new CurrentHealthEffect(5, maxHealth * 0.1f));
            yield return new WaitForSeconds(current.AnimationDurations.EvadeTime);
        }        
        public IEnumerator BuffDamage(Controller current, Controller opponent)
        {
            AddSkillCooldown(current, ESkills.BuffDamage, 2);
            current.Animator.SetTrigger("Spellcast");
            float damage = current.UnitStats.GetStat(EStats.Damage);
            current.CharacterActiveEffects.AddEffect(new CurrentHealthEffect(2, damage));
            yield return new WaitForSeconds(current.AnimationDurations.EvadeTime);
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
        private void GetAttacked(Controller current, Controller opponent, bool disableAnimation = false, float skillMultiplier = 1)
        {
            if (CriticalCheck(current))
                opponent.TakeDamage(current.UnitStats.GetStat(EStats.Damage) * 2 * skillMultiplier);
            else
                opponent.TakeDamage(current.UnitStats.GetStat(EStats.Damage) * skillMultiplier);
            if (!disableAnimation)
                opponent.Animator.SetTrigger("Defend");
            current.ParticleSystems.evaded = false;
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
        #endregion Helper Methods
    } 
}