﻿using System.Collections;
using UnityEngine;

namespace TurnFSM
{
    public class Begin : State
    {
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            SetUpBattle();
            MessageSystem.Print("The battle has begun");
            yield return new WaitForSeconds(0.1f);
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
        private void SetUpBattle()
        {
            BattleSystem.Player.ResetHealth();
        }
    }
}