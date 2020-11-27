using System.Collections;
using UnityEngine;

public class PlayerTurn : State
{
    public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {
        MessageSystem.Print("Player Turn");
        yield break;
    }
    public override IEnumerator Attack()
    {
        float remainingHealth = BattleSystem.Enemy.TakeDamage(BattleSystem.Player.GetComponent<UnitStats>().GetStat(EStats.Damage));
        //EventManager.BattleStateChanged(BattleState.ENEMYTURN);
        if (remainingHealth <= 0)
        {
            BattleSystem.SetState(new Won(BattleSystem));
        }
        else
        {
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
        yield return new WaitForSeconds(2f);
    }
    public override IEnumerator Move(int i)
    {
        BattleSystem.Player.GetComponent<UnitMovement>().MoveUnit(i);
        BattleSystem.SetState(new EnemyTurn(BattleSystem));
        yield break;
    }
    public override IEnumerator Equip(IEquipable equipable)
    {
        BattleSystem.Player.GetComponent<EquippedItems>().Equip(equipable);
        BattleSystem.SetState(new EnemyTurn(BattleSystem));
        yield break;
    }
}