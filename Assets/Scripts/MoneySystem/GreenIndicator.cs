using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GreenIndicator : MonoBehaviour {

    [SerializeField] private Image indicator;

    private Coroutine fadeCoroutine;

    public void ShowIndicator() {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() {
        float duration = 1f;
        float elapsed = 0f;

        Color color = indicator.color;
        color.a = 0f;
        indicator.color = color;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 25f / 255f, elapsed / duration);
            indicator.color = color;
            yield return null;
        }

        color.a = 25f / 255f;
        indicator.color = color;

        fadeCoroutine = null;
    }

}