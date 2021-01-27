using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class CharacterActiveEffects : MonoBehaviour
    {
        public List<Effect> ActiveEffectList { get; private set; } = new List<Effect>();
        public void TriggerEffects()
        {
            List<Effect> tempEffectList = new List<Effect>(ActiveEffectList);
            foreach (Effect effect in tempEffectList)
            {
                effect.ResolveEffect(gameObject);
                if (effect.Duration == 0)
                    ActiveEffectList.Remove(effect);
            }
        }
        public void AddEffect(Effect effect)
        {
            ActiveEffectList.Add(effect);
        }
        public void Reset()
        {
            ActiveEffectList = new List<Effect>();
        }
    } 
}
