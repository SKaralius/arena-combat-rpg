using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class Controller : MonoBehaviour
    {
        public GameObject healthBarPrefab;
        private GameObject healthBarGO;
        private HealthBar healthBar;
        public float Health { get; private set; }
        private UnitStats myStats;
        public Cooldowns characterCooldowns;

        private void Awake()
        {
            myStats = GetComponent<UnitStats>();
            healthBarGO = Instantiate(healthBarPrefab, GameObject.Find("UI").transform);
            if (transform.localScale.x < 0)
            {
                healthBarGO.transform.position = new Vector2(healthBarGO.transform.position.x * -1, healthBarGO.transform.position.y);
                healthBarGO.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
                healthBarGO.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            }
            healthBar = healthBarGO.GetComponentInChildren<HealthBar>();
            healthBar.unitStats = myStats;
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
            healthBar.UpdateHealthBar(Health);
        }

        private void Die()
        {
            MessageSystem.Print("Enemy is dead");
            //Destroy(gameObject);
        }
    }
}