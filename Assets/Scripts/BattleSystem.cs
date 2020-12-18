using TurnFSM;
using Unit;
using UnityEngine;

public class BattleSystem : StateMachine
{
    public Controller Player;
    public Controller Enemy;
    public GameObject healthBarPrefab;
    public GameObject skillsContainer;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        Player.gameObject.GetComponent<SkillManager>().enabled = true;
    }
    // Start is called before the first frame update
    private void Start()
    {
        SetState(new Begin(this));
    }

    public void OnAttackButton()
    {
        if (IsOpponentWithinAttackRange(Player))
        {
            StartCoroutine(State.Attack());
        }
        else
        {
            MessageSystem.Print("Out of range");
        }
    }

    public void OnMoveButton(int i)
    {
        StartCoroutine(State.Move(i));
    }

    public void OnSkillButton(Skill.UseSkillHandler skill)
    {
        StartCoroutine(State.UseSkill(skill));
    }

    public void OnItemEquipButton(EquippableItem item)
    {
        StartCoroutine(State.Equip(item));
    }

    public float GetDistanceBetweenFighters()
    {
        return Mathf.Abs(Player.gameObject.transform.position.x - Enemy.gameObject.transform.position.x);
    }

    public bool IsOpponentWithinAttackRange(Controller current)
    {
        float distance = GetDistanceBetweenFighters();
        return distance < current.GetComponent<UnitStats>().GetStat(EStats.AttackRange);
    }
    private void OnDestroy()
    {
        if (Player)
            Player.gameObject.GetComponent<SkillManager>().enabled = false;
    }

}