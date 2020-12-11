using System.Collections;
using UnityEngine.SceneManagement;
using Unit;

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
            BattleSystem.Player.GetComponent<UnitStats>().ResetModifiers();
            BattleSystem.Player.GetComponent<CharacterActiveEffects>().Reset();
            BattleSystem.Enemy.GetComponent<UnitStats>().ResetModifiers();
            BattleSystem.Enemy.GetComponent<CharacterActiveEffects>().Reset();

            SaverLoader.instance.SaveInventory();
            SceneManager.LoadSceneAsync("TownScene", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("CombatScene");
            return base.Start();
        }
    }
}