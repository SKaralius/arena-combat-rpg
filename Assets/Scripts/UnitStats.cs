using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour, IStats
{
    public float[] Stats { get; set; } = { 5,5,5,5,5 };
    private EquippedItems eqItems;
    private void Awake()
    {
        eqItems = GetComponent<EquippedItems>();
    }
    public float GetDamage()
    {
        return GetEquipmentStats(EStats.Damage);
    }

    public float GetMoveSpeed()
    {
        return GetEquipmentStats(EStats.MoveSpeed);
    }
    
    private float GetEquipmentStats(EStats stat)
    {
        float tempStat = Stats[(int)stat];
        foreach (IStats item in eqItems.equipedItems)
        {
            if (item != null)
                tempStat += item.Stats[(int)stat];
        }
        return tempStat;
    }
}
