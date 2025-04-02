using System.Collections;
using TMPro;
using UnityEngine;

public class FadeTextIn : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public float duration = 1.5f;

    private void OnEnable()
    {
        
        textMeshPro = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {
        Color color = textMeshPro.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            textMeshPro.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        textMeshPro.color = color;
    }
}
