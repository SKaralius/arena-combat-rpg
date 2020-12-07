using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public BattleSystem battleSystem;
    public GameObject skillPrefab;
    private GameObject[] skillSlots = new GameObject[4];
    // Start is called before the first frame update
    void Awake()
    {
        int i = 0;
        foreach (Transform skillLocation in transform)
        {
            skillSlots[i] = Instantiate(skillPrefab, skillLocation);
            skillSlots[i].GetComponent<Button>().onClick.AddListener(() => battleSystem.OnSkillButton(() => MessageSystem.Print("Hello world")));
            i++;
        }
    }
}
