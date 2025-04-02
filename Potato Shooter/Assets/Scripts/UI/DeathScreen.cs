using System.Collections;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private float fadeInTime;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on DeathScreen!");
        }
    }

    private void OnEnable()
    {
        StartCoroutine(FadeInAfterDelay());
    }

    private IEnumerator FadeInAfterDelay()
    {
        canvasGroup.alpha = 0;

        yield return new WaitForSeconds(delayTime);
        float duration = fadeInTime;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}
