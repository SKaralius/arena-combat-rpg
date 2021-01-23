using UnityEngine;

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
                target.GetComponent<Controller>().UnitStats.StatModifiers.SetStat(StatToChange, StatChangeAmount);
                effectApplied = true;
            }
            Duration--;
            if (Duration == 0)
            {
                target.GetComponent<Controller>().UnitStats.StatModifiers.SetStat(StatToChange, -StatChangeAmount);
            }
        }
    }
}