using System.Collections;
using UnityEngine;
using Unit;

namespace TurnFSM
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }
        public override IEnumerator Start()
        {
            MessageSystem.Print("Player Turn");
            yield break;
        }
        public override IEnumerator UseSkill(Skills.UseSkillHandler skill)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
            yield return BattleSystem.StartCoroutine(skill(BattleSystem, BattleSystem.Player, BattleSystem.Enemy));

            DecideNextState();
        }
        public override IEnumerator Attack()
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));

            yield return BattleSystem.StartCoroutine(Skills.instance.BasicAttack(BattleSystem, 0, BattleSystem.Player, BattleSystem.Enemy));

            DecideNextState();
        }
        public override IEnumerator Move(int i)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
            BattleSystem.Player.GetComponent<Animator>().SetBool("isWalking", true);
            BattleSystem.Player.MoveUnit(i);
            yield return new WaitForSeconds(1f);
            BattleSystem.Player.GetComponent<Animator>().SetBool("isWalking", false);
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
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