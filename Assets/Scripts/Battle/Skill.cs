using System.Collections;
using Unit;

namespace Battle
{
    public delegate IEnumerator UseSkillHandler(Controller current, Controller opponent = null);
    public class Skill
    {

        public string Name { get; }
        public UseSkillHandler Effect { get; }
        public float SkillRange{ get; }
        public ESkillType SkillType { get; }


        public Skill(UseSkillHandler effect, string name, float skillRange, ESkillType skillType)
        {
            Effect = effect;
            Name = name;
            SkillRange = skillRange;
            SkillType = skillType;
        }
    } 
}