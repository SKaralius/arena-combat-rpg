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
            //if (BattleSystem.IsOpponentWithinAttackRange(BattleSystem.Player))
            //{
            //    GameObject.Find("Attack").GetComponent<Image>().color = Color.black;
            //    SkillManager.instance.EnableAllSkills();
            //}
            //else
            //{
            //    GameObject.Find("Attack").GetComponent<Image>().color = Color.red;
            //    SkillManager.instance.DisableOutOfRangeSkills();
            //}
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
            BattleSystem.SetState(new ActionChosen(BattleSystem));

            yield return BattleSystem.StartCoroutine(Skills.instance.BasicAttack(BattleSystem, BattleSystem.Player, BattleSystem.Enemy, 0));

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