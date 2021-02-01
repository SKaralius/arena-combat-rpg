using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{

    public class CurrentHealthEffect : Effect
    {
        public float HealthChangePerTurn { get; private set; }
        public CurrentHealthEffect(int _duration, float _healthChangePerTurn) : base(_duration)
        {
            HealthChangePerTurn = _healthChangePerTurn;
        }

        public override void ResolveEffect(GameObject target)
        {
            Controller controller = target.GetComponent<Controller>();
            float maxHealth = controller.UnitStats.GetStat(EStats.Health);
            if (maxHealth > controller.Health)
            {
                controller.TakeDamage(-HealthChangePerTurn);
                controller.DamageDisplay.ShowDamage(-HealthChangePerTurn);
            }
            Duration -= 1;
        }

        public override string GetEffectType()
        {
            if (HealthChangePerTurn > 0)
                return "Health ▲";
            else
                return "Health ▼";
        }
    }

}