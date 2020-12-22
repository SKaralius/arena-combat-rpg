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
            BattleSystem.Player.GetComponent<CharacterActiveEffects>().TriggerEffects();
            // TODO: Check if player dead, set lost state if is
            BattleSystem.Player.characterCooldowns.ReduceAllCooldownsByOne();
            BattleSystem.Enemy.characterCooldowns.ReduceAllCooldownsByOne();
            BattleSystem.GetComponent<SkillManager>().RenderSkillCooldowns();
            if (!BattleSystem.IsOpponentWithinAttackRange(BattleSystem.Player))
            { 
                BattleSystem.GetComponent<SkillManager>().DisableOutOfRangeSkills();
            }
            MessageSystem.Print("Player Turn");
            yield break;
        }

        public override IEnumerator UseSkill(UseSkillHandler skill)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
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