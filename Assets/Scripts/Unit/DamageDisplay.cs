using System.Collections;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    private static readonly int numberOfDamageDisplaySlots = 5;
    private GameObject[] damageDisplaySlots = new GameObject[numberOfDamageDisplaySlots];
    private (TMP_Text, Animator)[] textsAndAnimators = new (TMP_Text, Animator)[numberOfDamageDisplaySlots];
    [SerializeField] private GameObject DamageTextLocation;
    [SerializeField] private GameObject DamageTextContainerPrefab;
    private float damageOffset = 0;

    private void Awake()
    {
        for (int i = 0; i < numberOfDamageDisplaySlots; i++)
        {
            damageDisplaySlots[i] = Instantiate(DamageTextContainerPrefab, DamageTextLocation.transform);
            damageDisplaySlots[i].SetActive(false);
            damageDisplaySlots[i].transform.localPosition = Vector3.zero;
            textsAndAnimators[i].Item1 = damageDisplaySlots[i].GetComponentInChildren<TMP_Text>();
            textsAndAnimators[i].Item2 = damageDisplaySlots[i].GetComponentInChildren<Animator>();
        }
    }

    public void ShowDamage(float damage)
    {
        damageOffset += 2f;
        if (damageOffset > 8)
            damageOffset = 0;
        if (transform.localScale.x > 0)
            damageOffset *= -1;

        string damageText = GetDamageText(damage);

        StartCoroutine(ActiveDamageDisplayInstance(damageText));
    }

    private string GetDamageText(float damage)
    {
        string damageText = "";
        if (damage > 0)
        {
            damageText = "-" + Mathf.Ceil(damage).ToString();
        }
        else
            damageText = "+" + Mathf.Ceil((-1 * damage)).ToString();
        return damageText;
    }

    private IEnumerator ActiveDamageDisplayInstance(string damageText)
    {
        int index = GetNextDisabledInstanceIndex();
        if (index == -1)
            MessageSystem.Print("Lack of damage display instances. Check DamageDisplay.cs");
        damageDisplaySlots[index].SetActive(true);

        damageDisplaySlots[index].transform.localPosition = new Vector3(
        damageDisplaySlots[index].transform.localPosition.x + damageOffset,
        damageDisplaySlots[index].transform.localPosition.y + UnityEngine.Random.Range(-5f, 5f),
        damageDisplaySlots[index].transform.localPosition.z);

        textsAndAnimators[index].Item1.text = damageText;
        float clipLength = textsAndAnimators[index].Item2.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        yield return new WaitForSeconds(clipLength);
        damageDisplaySlots[index].SetActive(false);
        damageDisplaySlots[index].transform.localPosition = Vector3.zero;
    }

    private int GetNextDisabledInstanceIndex()
    {
        for (int i = 0; i < damageDisplaySlots.Length; i++)
        {
            if (damageDisplaySlots[i].activeSelf == false)
                return i;
        }
        return -1;
    }
}