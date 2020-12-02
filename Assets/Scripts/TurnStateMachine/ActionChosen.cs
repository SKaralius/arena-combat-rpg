using System.Collections;

namespace TurnFSM
{
    internal class ActionChosen : State
    {
        public ActionChosen(BattleSystem battleSystem) : base(battleSystem)
        {
        }
        public override IEnumerator Start()
        {
            MessageSystem.Print("Action chosen");
            return base.Start();
        }
    }
}