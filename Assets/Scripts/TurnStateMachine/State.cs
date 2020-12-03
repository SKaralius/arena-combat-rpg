using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnFSM
{
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
        public virtual IEnumerator Equip(IItem item)
        {
            IEquipable equipableItem = (IEquipable)item;
            BattleSystem.Player.GetComponent<EquippedItems>().Unequip(equipableItem.Slot);
            MessageSystem.Print("Not player turn");
            yield break;
        }
    }
}