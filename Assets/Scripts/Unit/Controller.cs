using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class Controller : MonoBehaviour
    {
        public float Health { get; private set; }

        public UnitStats UnitStats { get; private set; }
        public UnitMovement UnitMovement { get; private set; }
        public Animator Animator { get; private set; }
        public AnimationDurations AnimationDurations { get; private set; }
        public CharacterActiveEffects CharacterActiveEffects { get; private set; }
        public ParticleSystems ParticleSystems { get; private set; }

        public HealthBar healthBar;
        public Cooldowns characterCooldowns;

        private void Awake()
        {
            UnitStats = GetComponent<UnitStats>();
            UnitMovement = GetComponent<UnitMovement>();
            Animator = GetComponentInChildren<Animator>();
            AnimationDurations = GetComponentInChildren<AnimationDurations>();
            CharacterActiveEffects = GetComponent<CharacterActiveEffects>();
            ParticleSystems = GetComponentInChildren<ParticleSystems>();
        }

        private void Start()
        {
            characterCooldowns = new Cooldowns();
            Health = UnitStats.GetStat(EStats.Health);
        }

        public void TakeDamage(float _damage)
        {
            Health -= _damage / 100 * (100 - UnitStats.GetStat(EStats.Armor));
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            healthBar.UpdateHealthBar(Health);
        }

        public void ResetHealth()
        {
            Health = UnitStats.GetStat(EStats.Health);
        }

        private void Die()
        {
            MessageSystem.Print("Enemy is dead");
            //Destroy(gameObject);
        }
    }
}