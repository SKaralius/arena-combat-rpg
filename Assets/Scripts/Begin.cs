using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {

    }
    // Start is called before the first frame update
    public override IEnumerator Start()
    {
        MessageSystem.Print("The battle has begun");
        yield return new WaitForSeconds(2f);
        BattleSystem.SetState(new PlayerTurn(BattleSystem));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
