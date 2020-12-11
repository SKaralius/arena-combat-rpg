using System.Collections;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace TurnFSM
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.Player.GetComponent<CharacterActiveEffects>().TriggerEffects();
            // TODO: Check if player dead, set lost state if is
            BattleSystem.Player.characterCooldowns.ReduceAllCooldownsByOne();
            BattleSystem.Enemy.characterCooldowns.ReduceAllCooldownsByOne();
            if (BattleSystem.IsOpponentWithinAttackRange(BattleSystem.Player))
            {
                //SkillManager.instance.EnableAllSkills();
                BattleSystem.Player.GetComponent<SkillManager>().RenderSkillCooldowns();
            }
            else
            {
                BattleSystem.Player.GetComponent<SkillManager>().DisableOutOfRangeSkills();
            }
            MessageSystem.Print("Player Turn");
            yield break;
        }

        public override IEnumerator UseSkill(Skill.UseSkillHandler skill)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
            yield return BattleSystem.StartCoroutine(skill(BattleSystem, BattleSystem.Player, BattleSystem.Enemy));

            DecideNextState();
        }

        public override IEnumerator Attack()
        {
            yield break;
            //BattleSystem.SetState(new ActionChosen(BattleSystem));

            //yield return BattleSystem.StartCoroutine(Skills.instance.BasicAttack(BattleSystem, BattleSystem.Player, BattleSystem.Enemy, 0));

            //DecideNextState();
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