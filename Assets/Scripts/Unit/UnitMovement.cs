using System.Collections;
using UnityEngine;

namespace Unit
{
    public class UnitMovement : MonoBehaviour
    {
        private float lerpDuration = 1f;

        public IEnumerator MoveUnit(float positionX)
        {
            float timeElapsed = 0;
            Vector2 startValue = transform.position;
            Vector2 endValue = new Vector2(positionX, transform.position.y);

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