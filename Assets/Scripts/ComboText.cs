using System.Collections;
using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_Text text;  // Only ONE reference

    [Header("Animation Settings")]
    [SerializeField] private float popScale = 1.3f;
    [SerializeField] private float popDuration = 0.25f;
    [SerializeField] private float fadeDuration = 0.75f;

    private Vector3 originalScale;
    private Coroutine currentRoutine;

    void Awake()
    {
        // Auto-assign if not set in Inspector
        if (!text)
            text = GetComponent<TMP_Text>();

        originalScale = transform.localScale;
        text.alpha = 0f;
    }

    public void Show(string message, Color color)
    {
        text.text = message;
        text.color = color;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        // Make visible and reset position/scale
        text.alpha = 1f;
        transform.localScale = originalScale * popScale;

        // Pop animation
        float t = 0f;
        while (t < popDuration)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale * popScale, originalScale, t / popDuration);
            yield return null;
        }

        // Hold for a short time before fading (optional)
        yield return new WaitForSeconds(0.5f);

        // Fade out animation
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            text.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        text.alpha = 0f;
        transform.localScale = originalScale;
    }
}
