using System.Collections;
using Battle;
using UnityEngine;
using Inventory;


namespace TurnFSM
{
    public class Won : State
    {
        public Won(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            MessageSystem.Print("Player has won the match");
            ResetCharacters();
            CleanScene();
            GameObject.Find("UI").GetComponent<UIContainer>().gold.ChangeGold(100);
            GameObject.Find("GameManager").GetComponent<GameManager>().nextEncounterNumber += 1;
            LoadTown();
            return base.Start();
        }
    }
}