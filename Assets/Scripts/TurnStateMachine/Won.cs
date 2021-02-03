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
            GameObject.Find("UI").GetComponent<UIContainer>().gold.ChangeGold(500);
            GameObject.Find("UI").transform.GetChild(0).GetComponent<InventoryManager>().AddItemToInventory(ItemGenerator.GenerateItem(GameManager.instance.GetTier()));
            GameObject.Find("GameManager").GetComponent<GameManager>().nextEncounterNumber += 1;
            LoadTown();
            return base.Start();
        }
    }
}