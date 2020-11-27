using System.Collections;

public class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {
        MessageSystem.Print("Player has won the match");
        return base.Start();
    }
}