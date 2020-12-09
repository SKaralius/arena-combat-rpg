using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnFSM
{
    public class EnemyTurn : State
    {
        private Dictionary<AIOrders, Func<IEnumerator>> orderList = new Dictionary<AIOrders, Func<IEnumerator>>();

        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            orderList[AIOrders.MoveLeft] = () => Move(-1);
            orderList[AIOrders.MoveRight] = () => Move(1);
            orderList[AIOrders.Attack] = Attack;
            // attack player, check if dead, if dead, set a lost state
            // if not, set player turn state
            MessageSystem.Print("Enemy Turn");
            AIOrders order = EnemyAI.DecideOrder(BattleSystem);
            yield return orderList[order]();
        }

        public override IEnumerator Move(int i)
        {
            BattleSystem.SetState(new ActionChosen(BattleSystem));
            BattleSystem.Enemy.GetComponent<Animator>().SetBool("isWalking", true);
            BattleSystem.Enemy.MoveUnit(i);
            yield return new WaitForSeconds(1f);
            BattleSystem.Enemy.GetComponent<Animator>().SetBool("isWalking", false);
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }

        public override IEnumerator Attack()
        {
            yield return BattleSystem.StartCoroutine(Skills.instance.BasicAttack(BattleSystem, 0, BattleSystem.Enemy, BattleSystem.Player));
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}