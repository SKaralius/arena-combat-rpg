using UnityEngine;

namespace Unit
{
    public class HealthBar : MonoBehaviour
    {
        private GameObject healthFill;
        private UnitStats myStats;

        // Start is called before the first frame update
        private void Awake()
        {
            healthFill = transform.Find("Fill").gameObject;
            myStats = transform.parent.GetComponent<UnitStats>();
        }

        private void Start()
        {
            UpdateHealthBar(myStats.GetStat(EStats.Health));
        }

        public void UpdateHealthBar(float currentHealth)
        {
            healthFill.transform.localScale = new Vector2(transform.Find("Background").transform.localScale.x / myStats.GetStat(EStats.Health) * currentHealth, healthFill.transform.localScale.y);
        }
    }
}