using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    public class HealthBar : MonoBehaviour
    {
        public UnitStats unitStats;
        [SerializeField] Image healthFillImage;
        [SerializeField] GameObject background;
        [SerializeField] GameObject fill;
        [SerializeField] GameObject outline;
        private float maxHealth;
        private float heightOfFill;

        private void Start()
        {
            heightOfFill = healthFillImage.sprite.rect.height;
            maxHealth = unitStats.GetStat(EStats.Health);
            UpdateHealthBar(maxHealth);
        }

        public void UpdateHealthBar(float currentHealth)
        {
            maxHealth = unitStats.GetStat(EStats.Health);
            float currentHPasPercent = currentHealth / maxHealth * 100;
            float distanceToMoveMask = heightOfFill / 100 * (100 - currentHPasPercent);
            SetParentOfHealthItems(transform.parent);
            transform.localPosition = new Vector2(transform.localPosition.x, -distanceToMoveMask);
            SetParentOfHealthItems(transform);
        }
        private void SetParentOfHealthItems(Transform transform)
        {
            // Order matters, to preserve sorting
            background.transform.SetParent(transform, true);
            fill.transform.SetParent(transform, true);
            outline.transform.SetParent(transform, true);
        }
    }
}