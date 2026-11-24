using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PaymentMachine : MonoBehaviour {

    [SerializeField] private Image indicator;

    private Coroutine fadeCoroutine;

    private Coroutine flashCoroutine;

    public void ShowIndicator() {
        if (indicator == null) {
            Debug.LogError("Indicator Image is not assigned!");
            return;
        }

        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(ScannerFlash());
    }

    private IEnumerator ScannerFlash() {
        float fadeInDuration = 0.15f;
        float holdDuration = 0.12f;
        float fadeOutDuration = 0.35f;

        float maxAlpha = 40f / 255f;
        float elapsed = 0f;

        Color color = indicator.color;

        // Start invisible
        color.a = 0f;
        indicator.color = color;

        // Fade in: 0 -> 40
        while (elapsed < fadeInDuration) {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                maxAlpha,
                Mathf.Clamp01(elapsed / fadeInDuration)
            );

            indicator.color = color;
            yield return null;
        }

        // Hold at peak brightness
        color.a = maxAlpha;
        indicator.color = color;
        yield return new WaitForSeconds(holdDuration);

        // Fade out: 40 -> 0
        elapsed = 0f;

        while (elapsed < fadeOutDuration) {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(
                maxAlpha,
                0f,
                Mathf.Clamp01(elapsed / fadeOutDuration)
            );

            indicator.color = color;
            yield return null;
        }

        // Ensure fully invisible
        color.a = 0f;
        indicator.color = color;

        flashCoroutine = null;
    }
}