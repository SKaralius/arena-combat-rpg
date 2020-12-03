using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class Controller : MonoBehaviour
    {
        public GameObject healthBarPrefab;
        private GameObject healthBar;
        private HealthBar hb;
        private float health;
        private UnitStats myStats;
        private EventManager.ItemEquipHandler healthBarHandler;
        private void Awake()
        {
            myStats = GetComponent<UnitStats>();
            healthBar = Instantiate(healthBarPrefab, transform);
            hb = healthBar.GetComponent<HealthBar>();
            healthBarHandler = (IItem item, int who) => { hb.UpdateHealthBar(health); };
            EventManager.OnItemEquipped += healthBarHandler;
            healthBar.transform.position = new Vector2(transform.position.x, transform.position.y);
            healthBar.transform.localPosition = new Vector2(healthBar.transform.localPosition.x - 0.2f, healthBar.transform.localPosition.y + 0.15f);
        }
        private void Start()
        {
            health = myStats.GetStat(EStats.Health);
        }
        public float TakeDamage(Controller attacker)
        {
            UnitStats stats = attacker.GetComponent<UnitStats>();
            bool evaded = Random.Range(0, 100) < myStats.GetStat(EStats.Evasion);
            if (evaded)
            {
                MessageSystem.Print("Attack was evaded");
                return health;
            }
            else
            {
                health -= Mathf.Clamp((stats.GetStat(EStats.Damage) - myStats.GetStat(EStats.Armor)), 0, 999);
                if (health <= 0)
                {
                    Die();
                }
                hb.UpdateHealthBar(health);
                return health;
            }
        }

        public void MoveUnit(int i)
        {
            GetComponent<UnitMovement>().MoveUnit(i);
        }

        private void Die()
        {
            Destroy(gameObject);
        }
        private void OnDestroy()
        {
            EventManager.OnItemEquipped -= healthBarHandler;
        }
    }
}
