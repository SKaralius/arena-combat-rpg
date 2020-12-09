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
        private EventManager.ItemEquipHandler healthBarHandler;

        private void Awake()
        {
            myStats = GetComponent<UnitStats>();
            healthBar = Instantiate(healthBarPrefab, transform);
            hb = healthBar.GetComponent<HealthBar>();
            healthBarHandler = (EquippableItem item, int who) => { hb.UpdateHealthBar(Health); };
            EventManager.OnItemEquipped += healthBarHandler;
            healthBar.transform.position = new Vector2(transform.position.x, transform.position.y);
            healthBar.transform.localPosition = new Vector2(healthBar.transform.localPosition.x - 0.2f, healthBar.transform.localPosition.y + 0.15f);
        }

        private void Start()
        {
            Health = myStats.GetStat(EStats.Health);
        }

        public bool TakeDamage(Controller attacker)
        {
            UnitStats stats = attacker.GetComponent<UnitStats>();
            bool evaded = Random.Range(0, 100) < myStats.GetStat(EStats.Evasion);
            if (evaded)
            {
                MessageSystem.Print("Attack was evaded");
                return evaded;
            }
            else
            {
                Health -= Mathf.Clamp((stats.GetStat(EStats.Damage) - myStats.GetStat(EStats.Armor)), 0, 999);
                if (Health <= 0)
                {
                    Health = 0;
                    Die();
                }
                hb.UpdateHealthBar(Health);
                return evaded;
            }
        }

        private void Die()
        {
            MessageSystem.Print("Enemy is dead");
            //Destroy(gameObject);
        }

        private void OnDestroy()
        {
            EventManager.OnItemEquipped -= healthBarHandler;
        }
    }
}