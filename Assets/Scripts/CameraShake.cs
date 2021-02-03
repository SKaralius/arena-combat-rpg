using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	private Transform camTransform;

	private float shakeDuration = 0f;
	private readonly float shakeAmount = 1.4f;
	private readonly float decreaseFactor = 1.0f;

	Vector3 originalPos;

    #region Singleton logic

    public static CameraShake instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        #endregion Singleton logic
        camTransform = gameObject.transform;
        originalPos = gameObject.transform.localPosition;
	}

	public IEnumerator Shake()
    {
        shakeDuration = 0.3f;
		while (shakeDuration > 0)
        {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
            yield return null;
        }
		shakeDuration = 0f;
		camTransform.localPosition = originalPos;
    }
}