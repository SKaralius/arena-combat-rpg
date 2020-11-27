using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected BattleSystem BattleSystem;

    public State(BattleSystem battleSystem)
    {
        BattleSystem = battleSystem;
    }
    // Start is called before the first frame update
    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Attack()
    {
        yield break;
    }
    public virtual IEnumerator Move(int i)
    {
        yield break;
    }
    public virtual IEnumerator Equip(IEquipable equipable)
    {
        yield break;
    }
}
