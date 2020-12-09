using System.Collections;
using Unit;

public class Skill
{
    public delegate IEnumerator UseSkillHandler(BattleSystem battleSystem, Controller current, Controller opponent);

    public UseSkillHandler effect;
    public bool isAffectedByRange;

    public Skill(UseSkillHandler _effect, bool _isAffectedByRange = false)
    {
        effect = _effect;
        isAffectedByRange = _isAffectedByRange;
    }
}