using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unit;

public class OverTimeEffectManager : MonoBehaviour
{
    [SerializeField] private bool isPlayer = false;
    private CharacterActiveEffects characterActiveEffects;
    [SerializeField] private OverTimeEffectSlotUI slotUIPrefab;
    private void Awake()
    {
        if (isPlayer)
            characterActiveEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterActiveEffects>();
        else
            characterActiveEffects = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CharacterActiveEffects>();
    }
    private void Start()
    {
        RefreshOverTimeEffectUI();
    }
    public void RefreshOverTimeEffectUI()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(Effect effect in characterActiveEffects.ActiveEffectList)
        {
            OverTimeEffectSlotUI slotUI = Instantiate(slotUIPrefab, this.transform).GetComponent<OverTimeEffectSlotUI>();
            slotUI.effectImage = null;
            slotUI.effectName.text = effect.GetEffectType();
            slotUI.effectDuration.text = effect.Duration.ToString();
        }
    }
}
