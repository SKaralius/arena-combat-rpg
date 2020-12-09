using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class CharacterSkills : MonoBehaviour
    {
        public List<Skills.ESkills> characterSkills = new List<Skills.ESkills>();

        private void Start()
        {
            EventManager.OnItemEquipped += AddToSkillList;
            EventManager.OnItemUnequipped += RemoveFromSkillList;
        }

        public void AddToSkillList(EquippableItem item, int who)
        {
            if (item.Skill != Skills.ESkills.None)
            {
                characterSkills.Add(item.Skill);
                if (SkillManager.instance)
                    SkillManager.instance.RenderSkillSlots();
            }
        }

        public void RemoveFromSkillList(EquippableItem item, int who)
        {
            if (item.Skill != Skills.ESkills.None)
            {
                characterSkills.Remove(item.Skill);
                if (SkillManager.instance)
                    SkillManager.instance.RenderSkillSlots();
            }
        }

        private void OnDestroy()
        {
            EventManager.OnItemEquipped -= AddToSkillList;
            EventManager.OnItemUnequipped -= RemoveFromSkillList;
        }
    } 
}