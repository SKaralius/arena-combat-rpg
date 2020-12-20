using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class CharacterSkills : MonoBehaviour
    {
        public List<ESkills> characterSkills = new List<ESkills>();

        private void Awake()
        {
            // Add default skills that should be available to all characters.
            characterSkills.Add(ESkills.MoveBackwards);
            characterSkills.Add(ESkills.BasicAttack);
            characterSkills.Add(ESkills.MoveForwards);
        }

        public void AddToSkillList(EquippableItem item)
        {
            if (item.Skill != ESkills.None)
            {
                characterSkills.Add(item.Skill);
            }
        }

        public void RemoveFromSkillList(EquippableItem item)
        {
            if (item.Skill != ESkills.None)
            {
                characterSkills.Remove(item.Skill);
            }
        }
    } 
}