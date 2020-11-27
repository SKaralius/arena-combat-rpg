using System.Collections;

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