using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class Controller : MonoBehaviour
    {
        public HealthBar healthBar;
        public float Health { get; private set; }
        private UnitStats myStats;
        public Cooldowns characterCooldowns;

        private void Awake()
        {
            myStats = GetComponent<UnitStats>();
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

        public void ResetHealth()
        {
            Health = myStats.GetStat(EStats.Health);
        }

        private void Die()
        {
            MessageSystem.Print("Enemy is dead");
            //Destroy(gameObject);
        }
    }
}