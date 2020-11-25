using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject healthFill;
    // Start is called before the first frame update
    void Awake()
    {
        healthFill = transform.Find("Fill").gameObject;
        UpdateHealthBar(100);
    }
    public void UpdateHealthBar(float currentHealth)
    {
        healthFill.transform.localScale = new Vector2(transform.Find("Background").transform.localScale.x / 100 * currentHealth, healthFill.transform.localScale.y);
    }
}
