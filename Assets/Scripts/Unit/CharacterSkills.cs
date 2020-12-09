using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class CharacterSkills : MonoBehaviour
    {
        public List<ESkills> characterSkills = new List<ESkills>();

        private void Start()
        {
            EventManager.OnItemEquipped += AddToSkillList;
            EventManager.OnItemUnequipped += RemoveFromSkillList;
            // Add default skills that should be available to all characters.
            characterSkills.Add(ESkills.MoveBackwards);
            characterSkills.Add(ESkills.BasicAttack);
            characterSkills.Add(ESkills.MoveForwards);
            if (SkillManager.instance && GetComponent<EquippedItems>().isPlayer)
                SkillManager.instance.RenderSkillSlots();
        }

        public void AddToSkillList(EquippableItem item, int who)
        {
            if (who != gameObject.GetHashCode())
                return;
            if (item.Skill != ESkills.None)
            {
                characterSkills.Add(item.Skill);
                if (SkillManager.instance && GetComponent<EquippedItems>().isPlayer)
                    SkillManager.instance.RenderSkillSlots();
            }
        }

        public void RemoveFromSkillList(EquippableItem item, int who)
        {
            if (who != gameObject.GetHashCode())
                return;
            if (item.Skill != ESkills.None)
            {
                characterSkills.Remove(item.Skill);
                if (SkillManager.instance && GetComponent<EquippedItems>().isPlayer)
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