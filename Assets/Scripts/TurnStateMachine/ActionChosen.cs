﻿using System.Collections;
using Battle;

namespace TurnFSM
{
    internal class ActionChosen : State
    {
        public ActionChosen(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            BattleSystem.skillsContainer.SetActive(false);
            MessageSystem.Print("Action chosen");
            return base.Start();
        }
    }
}