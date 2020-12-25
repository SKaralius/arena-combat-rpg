using System.Collections;
using Battle;
using UnityEngine;
using Inventory;

namespace TurnFSM
{
    public class Lost : State
    {
        public Lost(BattleSystem battleSystem) : base(battleSystem)
        {
        }
        public override IEnumerator Start()
        {
            MessageSystem.Print("Player has lost the match");
            ResetCharacters();
            CleanScene();
            GameObject.Find("UI").GetComponent<UIContainer>().gold.ChangeGold(-500);
            LoadTown();
            return base.Start();
        }
    }

}