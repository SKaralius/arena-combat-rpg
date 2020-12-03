using System.Collections;
using UnityEngine;

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
        public override IEnumerator Attack()
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
            float remainingHealth = BattleSystem.Enemy.TakeDamage(BattleSystem.Player);
            BattleSystem.Player.GetComponent<Animator>().SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.3f);
            //EventManager.BattleStateChanged(BattleState.ENEMYTURN);
            BattleSystem.Player.GetComponent<Animator>().SetBool("isAttacking", false);
            if (remainingHealth <= 0)
            {
                BattleSystem.SetState(new Won(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new EnemyTurn(BattleSystem));
            }
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
        public override IEnumerator Equip(IItem equipable)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
            yield break;
        }
    }
}