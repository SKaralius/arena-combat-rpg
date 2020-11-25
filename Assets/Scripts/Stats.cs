using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float damage = 10f;
    private EquippedItems eqItems;
    private void Awake()
    {
        eqItems = GetComponent<EquippedItems>();
    }
    public float GetDamage()
    {
        float _damage = damage;
        if (!InventoryManager.instance)
            return _damage;
        if (eqItems.equipedItems[4] != null)
        {
            Weapon wep = (Weapon)eqItems.equipedItems[4];
            _damage += wep.Damage;
        }
        return _damage;
    }
}
