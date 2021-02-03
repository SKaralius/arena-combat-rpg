using System.Collections;
using Unit;
using UnityEngine;
using UnityEngine.UI;
using Battle;

namespace TurnFSM
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.skillsContainer.SetActive(true);

            // TODO: Check if player dead, set lost state if is
            BattleSystem.Player.characterCooldowns.ReduceAllCooldownsByOne();
            BattleSystem.Enemy.characterCooldowns.ReduceAllCooldownsByOne();
            BattleSystem.GetComponent<SkillManager>().RenderSkillCooldowns();
            float playerAttackRange = BattleSystem.Player.UnitStats.GetStat(EStats.AttackRange);
            BattleSystem.GetComponent<SkillManager>().DisableOutOfRangeSkills(BattleSystem.GetDistanceBetweenFighters(), playerAttackRange);
            MessageSystem.Print("Player Turn");
            yield break;
        }

        public override IEnumerator UseSkill(UseSkillHandler skill)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));

            BattleSystem.Enemy.GetComponent<CharacterActiveEffects>().TriggerEffects();
            BattleSystem.enemyOverTimeEffectManager.RefreshOverTimeEffectUI();
            BattleSystem.playerOverTimeEffectManager.RefreshOverTimeEffectUI();

            yield return BattleSystem.StartCoroutine(skill(BattleSystem.Player, BattleSystem.Enemy));


            DecideNextState();

        }

        public override IEnumerator Equip(EquippableItem equipable)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
            yield break;
        }

        protected override void DecideNextState()
        {
            if (BattleSystem.Enemy.GetComponent<Controller>().Health <= 0)
            {
                BattleSystem.SetState(new Won(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }
        }
    }
}