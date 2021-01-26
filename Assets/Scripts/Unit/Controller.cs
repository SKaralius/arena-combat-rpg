using System.Collections;
using UnityEngine;
using TMPro;
using System;

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

        public void TakeDamage(float damage)
        {
            float maxHealth = UnitStats.GetStat(EStats.Health);
            
            float damageVariation = UnityEngine.Random.Range(0, damage * 0.2f);
            damage += damage * 0.2f;
            damage -= damageVariation * 2;

            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }
            ShowDamage(damage);
            healthBar.UpdateHealthBar(Health);
        }
        // Tries to evade an attack
        // Compares evasion rating with opponent accuracy rating
        // Evasion rating to percentage is dynamic, based on progression. More progression into the game, requires more rating to achieve
        // same evasion percent.
        public bool TryEvade(float opponentAccuracyRating)
        {
            float ratingForSinglePercent = GetSinglePercentRatingValue();

            float accuracyBalanceRatio = 0.7f;
            float difference = UnitStats.GetStat(EStats.Evasion) - (opponentAccuracyRating * accuracyBalanceRatio);
            float evadePercentage = difference / ratingForSinglePercent;
            Debug.Log($"Accuracy rating: {opponentAccuracyRating}, evasionPercentage: {evadePercentage}");
            evadePercentage = Mathf.Clamp(evadePercentage, 0, 75);
            float evadeRoll = UnityEngine.Random.Range(0, 100);
            if (evadeRoll < evadePercentage)
                return true;
            else
                return false;
        }

        public float ReduceDamageWithArmor(float damage)
        {
            float ratingForSinglePercent = GetSinglePercentRatingValue();

            float armorRating = UnitStats.GetStat(EStats.Armor);
            float armorDamageReductionPercentage = armorRating / ratingForSinglePercent;
            armorDamageReductionPercentage = Mathf.Clamp(armorDamageReductionPercentage, 0, 75);
            float reducedDamage = damage / 100 * (100 - armorDamageReductionPercentage);
            return reducedDamage;
        }

        public bool TryCritical()
        {
            float ratingForSinglePercent = GetSinglePercentRatingValue();

            float criticalRating = UnitStats.GetStat(EStats.Critical);
            float criticalStrikePercentage = criticalRating / ratingForSinglePercent;
            criticalStrikePercentage = Mathf.Clamp(criticalStrikePercentage, 0, 75);
            float criticalRoll = UnityEngine.Random.Range(0, 100);
            if (criticalRoll < criticalStrikePercentage)
                return true;
            else
                return false;
        }

        public void ResetHealth()
        {
            Health = UnitStats.GetStat(EStats.Health);
        }

        private void Die()
        {
            // TODO: Death animation, sound etc.
            MessageSystem.Print("Enemy is dead");
        }
        public void ResetPosition()
        {
            transform.position = originalPostion;
        }
        private void ShowDamage(float damageAfterArmor)
        {
            // TODO: Fade out
            if (damageAfterArmor > 0)
                DamageText.text = "-" + Mathf.Ceil(damageAfterArmor).ToString();
            else
                DamageText.text = "+" + Mathf.Ceil((-1 * damageAfterArmor)).ToString();
        }

        private static float GetSinglePercentRatingValue()
        {
            float maxEquipmentStats = (ItemGenerator.statScoreBaseValue + GameManager.instance.nextEncounterNumber) * Enum.GetNames(typeof(EquipSlot)).Length;
            float numberOfStatsThatCanBeMaxed = 3f;
            float oneHundredPercentAtRating = maxEquipmentStats / numberOfStatsThatCanBeMaxed;
            float ratingForSinglePercent = oneHundredPercentAtRating / 100;
            return ratingForSinglePercent;
        }
    }
}