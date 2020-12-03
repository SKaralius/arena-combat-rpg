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
            SceneManager.LoadScene(1);
            return base.Start();
        }
    }
}