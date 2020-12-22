using TurnFSM;
using Unit;
using UnityEngine;

namespace Battle
{

    public class BattleSystem : StateMachine
    {
        public Controller Player;
        public Controller Enemy;
        public GameObject healthBarPrefab;
        public GameObject skillsContainer;
        public SpriteRenderer Background { get; private set; }

        private void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
            Background = GameObject.Find("BG").GetComponent<SpriteRenderer>();
            GameObject.Find("GameManager").GetComponent<Skills>().battleSystem = this;
        }
        // Start is called before the first frame update
        private void Start()
        {
            SetState(new Begin(this));
        }

        public void OnSkillButton(UseSkillHandler skill)
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
    } 
}