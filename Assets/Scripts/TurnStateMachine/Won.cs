using System.Collections;
using UnityEngine.SceneManagement;

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
            SaverLoader.instance.SaveInventory();
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(0);
            return base.Start();
        }
    }
}