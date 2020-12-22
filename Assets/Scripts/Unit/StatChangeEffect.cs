using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Unit
{
    public class StatChangeEffect : Effect
    {
        private bool effectApplied = false;
        public EStats StatToChange { get; private set; }
        public float StatChangeAmount { get; private set; }
        public StatChangeEffect(int _duration, EStats _statToChange, float _statChangeAmount) : base(_duration)
        {
            StatToChange = _statToChange;
            StatChangeAmount = _statChangeAmount;
        }
        public override void ResolveEffect(GameObject target)
        {
            if (!effectApplied)
            {
                target.GetComponent<UnitStats>().StatModifiers[(int)StatToChange] += StatChangeAmount;
                effectApplied = true;
            }
            Duration--;
            if (Duration == 0)
            {
                target.GetComponent<UnitStats>().StatModifiers[(int)StatToChange] -= StatChangeAmount;
            }
        }
    }

}