using System.Collections;
using UnityEngine;

namespace Unit
{
    public class UnitMovement : MonoBehaviour
    {
        private float lerpDuration = 1f;
        private UnitStats playerStats;

        private void Awake()
        {
            playerStats = GetComponent<UnitStats>();
        }

        public void MoveUnit(int isRight)
        {
            StartCoroutine(MoveUnitCoroutine(isRight));
        }

        private IEnumerator MoveUnitCoroutine(int isRight)
        {
            float timeElapsed = 0;
            Vector2 startValue = transform.position;
            Vector2 endValue = new Vector2(transform.position.x + (playerStats.GetStat(EStats.MoveSpeed) * isRight), transform.position.y);
            while (timeElapsed < lerpDuration)
            {
                transform.position = Vector2.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;

                yield return null;
            }
            transform.position = endValue;
        }
    }
}