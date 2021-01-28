using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;
using Battle;

namespace TurnFSM
{
    public class EnemyTurn : State
    {
        //private Dictionary<AIOrders, Func<IEnumerator>> orderList = new Dictionary<AIOrders, Func<IEnumerator>>();

        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            //orderList[AIOrders.MoveLeft] = () => Move(-1);
            //orderList[AIOrders.MoveRight] = () => Move(1);
            //orderList[AIOrders.Attack] = Attack;
            //orderList[AIOrders.UseSkill] = UseSkill();
            MessageSystem.Print("Enemy Turn");
            if (BattleSystem.Enemy.Health <= 0)
            {
                BattleSystem.SetState(new Won(BattleSystem));
                yield break;
            }
            UseSkillHandler order = BattleSystem.Enemy.GetComponent<EnemyAI>().DecideOrder(BattleSystem);
            yield return order(BattleSystem.Enemy, BattleSystem.Player);
            DecideNextState();

            BattleSystem.Player.GetComponent<CharacterActiveEffects>().TriggerEffects();
            BattleSystem.playerOverTimeEffectManager.RefreshOverTimeEffectUI();
            BattleSystem.enemyOverTimeEffectManager.RefreshOverTimeEffectUI();
        }

        protected override void DecideNextState()
        {
            if (BattleSystem.Player.GetComponent<Controller>().Health <= 0)
            {
                BattleSystem.SetState(new Lost(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
        }
    }
}