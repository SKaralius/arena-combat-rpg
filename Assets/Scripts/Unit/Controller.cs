using System.Collections;
using UnityEngine;

namespace Unit
{
    public class Controller : MonoBehaviour
    {
        public GameObject healthBarPrefab;
        private GameObject healthBar;
        private HealthBar hb;
        public float Health { get; private set; }
        private UnitStats myStats;
        public Cooldowns characterCooldowns;

        private void Awake()
        {
            myStats = GetComponent<UnitStats>();
            healthBar = Instantiate(healthBarPrefab, transform);
            hb = healthBar.GetComponent<HealthBar>();
            healthBar.transform.position = new Vector2(transform.position.x, transform.position.y);
            healthBar.transform.localPosition = new Vector2(healthBar.transform.localPosition.x - 0.2f, healthBar.transform.localPosition.y + 0.15f);
        }

        private void Start()
        {
            characterCooldowns = new Cooldowns();
            Health = myStats.GetStat(EStats.Health);
        }

        public void TakeDamage(float _damage)
        {
            Health -= Mathf.Clamp((_damage - myStats.GetStat(EStats.Armor)), 0, 999);
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            hb.UpdateHealthBar(Health);
        }

        private void Die()
        {
            MessageSystem.Print("Enemy is dead");
            //Destroy(gameObject);
        }
    }
}