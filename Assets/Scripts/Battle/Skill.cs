using System.Collections;
using Unit;

namespace Battle
{
    public delegate IEnumerator UseSkillHandler(Controller current, Controller opponent = null);
    public class Skill
    {

        public string Name { get; }
        public UseSkillHandler Effect { get; }
        public bool IsAffectedByRange { get; }


        public Skill(UseSkillHandler _effect, string _name, bool _isAffectedByRange = false)
        {
            Effect = _effect;
            Name = _name;
            IsAffectedByRange = _isAffectedByRange;
        }
    } 
}