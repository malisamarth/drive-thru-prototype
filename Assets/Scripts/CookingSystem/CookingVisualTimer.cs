using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CookingVisualTimer : MonoBehaviour {

    [SerializeField] private Image timerImage;

    private Coroutine timerCoroutine;

    private void Awake() {
        timerImage.fillAmount = 0f;
    }

    public void StartCookingTimer(float duration) {
        // Stop any currently running timer
        if (timerCoroutine != null) {
            StopCoroutine(timerCoroutine);
        }

        timerCoroutine = StartCoroutine(FillTimer(duration));
    }

    private IEnumerator FillTimer(float duration) {
        timerImage.fillAmount = 0f;

        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            timerImage.fillAmount = Mathf.Clamp01(elapsed / duration);

            yield return null;
        }

        timerImage.fillAmount = 1f;
        timerCoroutine = null;

        // Timer finished
        Debug.Log("Cooking timer finished!");

        timerImage.fillAmount = 0f;
    }

    public void StopTimer() {
        if (timerCoroutine != null) {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

}
