using System.Collections;
using UnityEngine;
using TMPro;

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

        [SerializeField] private TMP_Text DamageText;

        private Vector2 originalPostion;

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
            originalPostion = transform.position;
        }

        public void TakeDamage(float _damage)
        {
            float maxHealth = UnitStats.GetStat(EStats.Health);
            float damageAfterArmor = _damage / 100 * (100 - UnitStats.GetStat(EStats.Armor));
            Health -= damageAfterArmor;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }
            ShowDamage(damageAfterArmor);
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
        public void ResetPosition()
        {
            transform.position = originalPostion;
        }
        private void ShowDamage(float damageAfterArmor)
        {
            if (damageAfterArmor > 0)
                DamageText.text = "-" + Mathf.Ceil(damageAfterArmor).ToString();
            else
                DamageText.text = "+" + Mathf.Ceil((-1 * damageAfterArmor)).ToString();
        }
    }
}