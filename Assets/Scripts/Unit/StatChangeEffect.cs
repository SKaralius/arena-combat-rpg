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
            Controller controller = target.GetComponent<Controller>();
            if (!effectApplied)
            {
                controller.UnitStats.StatModifiers.SetStat(StatToChange, StatChangeAmount);
                effectApplied = true;
            }
            Duration--;
            if (Duration == 0)
            {
                controller.UnitStats.StatModifiers.SetStat(StatToChange, -StatChangeAmount);
            }
        }
        public override string GetEffectType()
        {
            string upOrDownArrow;
            if (StatChangeAmount > 0)
                upOrDownArrow = "▲";
            else
                upOrDownArrow = "▼";
            string message = $"{Stats.GetStatName(StatToChange)} {upOrDownArrow}";
            return message;
        }
    }
}