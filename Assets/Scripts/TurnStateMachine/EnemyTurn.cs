using System.Collections;

namespace TurnFSM
{
    public class EnemyTurn : State
    {
        public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }
        public override IEnumerator Start()
        {
            // attack player, check if dead, if dead, set a lost state
            // if not, set player turn state
            MessageSystem.Print("Enemy Turn");
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
            return base.Start();
        }

    }
}