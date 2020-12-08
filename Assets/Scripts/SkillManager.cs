using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public BattleSystem battleSystem;
    public GameObject skillPrefab;
    private CharacterSkills characterSkills;
    private GameObject[] skillSlots = new GameObject[4];
    #region Singleton logic
    public static SkillManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion
    void Start()
    {
        int i = 0;
        foreach (Transform skillLocation in transform)
        {
            skillSlots[i] = Instantiate(skillPrefab, skillLocation);
            i++;
        }
        characterSkills = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSkills>();
        RenderSkillSlots();
    }
    public void RenderSkillSlots()
    {
        int i = 0;
        foreach (Skills.ESkills skill in characterSkills.characterSkills)
        {
            skillSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            skillSlots[i].GetComponent<Button>().onClick.AddListener(() => battleSystem.OnSkillButton(Skills.instance.skillsList[skill]));
            i++;
        }
    }
}
