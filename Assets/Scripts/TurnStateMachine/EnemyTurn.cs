﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;

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
            BattleSystem.Enemy.GetComponent<CharacterActiveEffects>().TriggerEffects();
            if (BattleSystem.Enemy.Health <= 0)
            {
                BattleSystem.SetState(new Won(BattleSystem));
                yield break;
            }
            Skill.UseSkillHandler order = BattleSystem.Enemy.GetComponent<EnemyAI>().DecideOrder(BattleSystem);
            yield return order(BattleSystem.Enemy, BattleSystem.Player);
            DecideNextState();
        }

        public override IEnumerator UseSkill(Skill.UseSkillHandler skill)
        {
            yield return BattleSystem.StartCoroutine(skill(BattleSystem.Enemy, BattleSystem.Player));

            DecideNextState();
        }
        protected override void DecideNextState()
        {
            if (BattleSystem.Player.GetComponent<Controller>().Health <= 0)
            {
                // TODO: Implement a Lost State
                //BattleSystem.SetState(new Lost(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new PlayerTurn(BattleSystem));
            }
        }
    }
}