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
            target.GetComponent<Controller>().TakeDamage(HealthChangePerTurn);
            Duration -= 1;
        }
    }

}