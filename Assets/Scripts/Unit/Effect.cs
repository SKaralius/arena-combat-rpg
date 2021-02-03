using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{

    public abstract class Effect
    {
        public int Duration { get; set; }
        public Effect(int _duration)
        {
            Duration = _duration;
        }
        public abstract void ResolveEffect(GameObject target);
        public abstract string GetEffectType();
    }

}