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

    public void ShowDamage(float damage, bool isCritical = false)
    {
        damageOffset += 4f;
        if (damageOffset > 12)
            damageOffset = 0;
        if (transform.localScale.x > 0)
            damageOffset *= -1;

        string damageText = GetDamageText(damage, isCritical);
        int index = GetNextDisabledInstanceIndex();
        if (index == -1)
            MessageSystem.Print("Lack of damage display instances. Check DamageDisplay.cs");
        ApplyTextEffects(damage, index, isCritical);
        StartCoroutine(ActiveDamageDisplayInstance(damageText, index));
    }

    private string GetDamageText(float damage, bool isCritical = false)
    {
        string damageText;
        if (damage > 0)
        {
            damageText = "-" + Mathf.Ceil(damage).ToString();
        }
        else
            damageText = "+" + Mathf.Ceil((-1 * damage)).ToString();
        if (isCritical)
        {
            damageText += "!";
        }
        return damageText;
    }

    private void ApplyTextEffects(float damage, int index, bool isCritical = false)
    {
        Vector3 tmp = damageDisplaySlots[index].transform.rotation.eulerAngles;
        tmp.z = UnityEngine.Random.Range(-10f, 10f);
        damageDisplaySlots[index].transform.rotation = Quaternion.Euler(tmp);

        TMP_Text textObject = textsAndAnimators[index].Item1;
        if (damage < 0)
            textObject.color = Color.green;
        else
            textObject.color = Color.red;

        if (isCritical)
        {
            textObject.fontSize = 34;
            textObject.fontStyle = FontStyles.Italic;
        } else
        {
            textObject.fontSize = 24;
            textObject.fontStyle = FontStyles.Normal;
        }
    }

    private IEnumerator ActiveDamageDisplayInstance(string damageText, int index)
    {
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