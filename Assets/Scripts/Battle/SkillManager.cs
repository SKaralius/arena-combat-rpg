using TMPro;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{

    public class SkillManager : MonoBehaviour
    {
        private BattleSystem battleSystem;
        [SerializeField] private GameObject skillPrefab = default;
        private CharacterSkills characterSkills;
        private GameObject[] skillSlots = new GameObject[7];
        private GameObject skillsContainer;

        private void Awake()
        {
            characterSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSkills>();
            battleSystem = GetComponent<BattleSystem>();
            skillsContainer = GameObject.Find("Skills");
        }

        private void Start()
        {
            int i = 0;
            foreach (Transform skillLocation in skillsContainer.transform)
            {
                skillSlots[i] = Instantiate(skillPrefab, skillLocation);
                i++;
            }
            RenderSkillSlots();
            battleSystem.Player.characterCooldowns = new Cooldowns();
            battleSystem.Enemy.characterCooldowns = new Cooldowns();
        }

        public void RenderSkillSlots()
        {
            int i = 0;
            foreach (ESkills skill in characterSkills.characterSkills)
            {
                skillSlots[i].gameObject.SetActive(true);
                skillSlots[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Skills.instance.skillsList[skill].Name;
                skillSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                skillSlots[i].GetComponent<Button>().onClick.AddListener(() => battleSystem.OnSkillButton(Skills.instance.skillsList[skill].Effect));
                i++;
            }
            while (i < skillSlots.Length)
            {
                skillSlots[i].gameObject.SetActive(false);
                i++;
            }
        }

        public void DisableOutOfRangeSkills()
        {
            int i = 0;
            foreach (ESkills skill in characterSkills.characterSkills)
            {
                if (Skills.instance.skillsList[skill].IsAffectedByRange)
                {
                    skillSlots[i].GetComponent<Image>().color = Color.red;
                    int cooldown = battleSystem.Player.characterCooldowns.cooldowns[skill];
                    if (cooldown > 0)
                    {
                        skillSlots[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = cooldown.ToString();
                    }
                    skillSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    skillSlots[i].GetComponent<Button>().onClick.AddListener(() => MessageSystem.Print("Out of range"));
                }
                i++;
            }
        }

        public void RenderSkillCooldowns()
        {
            int i = 0;
            foreach (ESkills skill in characterSkills.characterSkills)
            {
                int cooldown = battleSystem.Player.characterCooldowns.cooldowns[skill];
                if (cooldown > 0)
                {
                    skillSlots[i].GetComponent<Image>().color = Color.grey;
                    skillSlots[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = cooldown.ToString();
                    skillSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    skillSlots[i].GetComponent<Button>().onClick.AddListener(() => MessageSystem.Print("On cooldown."));
                }
                else
                {
                    skillSlots[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Skills.instance.skillsList[skill].Name;
                    skillSlots[i].GetComponent<Image>().color = Color.white;
                    skillSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    skillSlots[i].GetComponent<Button>().onClick.AddListener(() => battleSystem.OnSkillButton(Skills.instance.skillsList[skill].Effect));
                }
                i++;
            }
        }
    } 
}