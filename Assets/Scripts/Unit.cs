using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public GameObject cam = null;
    private GameObject healthBar;
    private float health = 100f;
    private void Awake()
    {
        healthBar = Instantiate(healthBarPrefab, transform);
        healthBar.transform.position = new Vector2(transform.position.x, transform.position.y);
        healthBar.transform.localPosition = new Vector2(healthBar.transform.localPosition.x - 0.2f, healthBar.transform.localPosition.y + 0.15f);
        // If unit is player
        if (cam)
        {
            Instantiate(cam, transform);
        }
    }
    public float TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            Die();
        }
        healthBar.GetComponent<HealthBar>().UpdateHealthBar(health);
        return health;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
