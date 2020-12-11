using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class CharacterActiveEffects : MonoBehaviour
    {
        private List<Effect> activeEffectList = new List<Effect>();
        public void TriggerEffects()
        {
            List<Effect> tempEffectList = new List<Effect>(activeEffectList);
            foreach (Effect effect in tempEffectList)
            {
                effect.ResolveEffect(gameObject);
                if (effect.Duration == 0)
                    activeEffectList.Remove(effect);
            }
        }
        public void AddEffect(Effect effect)
        {
            activeEffectList.Add(effect);
        }
        public void Reset()
        {
            activeEffectList = new List<Effect>();
        }
    } 
}
