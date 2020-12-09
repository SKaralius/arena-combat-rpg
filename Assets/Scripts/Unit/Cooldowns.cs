using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class Cooldowns
    {
        public Dictionary<ESkills, int> cooldowns;

        public Cooldowns()
        {
            this.cooldowns = new Dictionary<ESkills, int>();
            foreach (ESkills skill in Skills.instance.skillsList.Keys)
            {
                cooldowns[skill] = 0;
            }
        }

        public void ReduceAllCooldownsByOne()
        {
            List<ESkills> keys = new List<ESkills>(cooldowns.Keys);
            foreach (ESkills skill in keys)
            {
                if (cooldowns[skill] > 0)
                    cooldowns[skill] -= 1;
            }
        }
        public void AddCooldownToSkill(ESkills skill, int turns)
        {
            cooldowns[skill] += turns;
        }
    } 
}
