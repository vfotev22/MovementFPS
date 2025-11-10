using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class comboText : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_Text Text;

     [Header("Animation Settings")] private TMP_Text text;
    [SerializeField] private float popScale = 1.3f;
    [SerializeField] private float popDuration = 0.25f;
    [SerializeField] private float fadeDuration = 0.75f;

    private Vector3 originalScale;
    private Coroutine currentRoutine;

     void Awake()
    {
        if(!Text) text = GetComponent<TMP_Text>();
        originalScale = transform.localScale;
        text.alpha = 0f;
    }

    public void ShowCombo(string message, Color color)
    {
        text.text = message;
        text.color = color;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
        
        currentRoutine = StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        // Pop Animation
        float t = 0f;
        while (t < popDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(popScale, 1f, t / popDuration);
            transform.localScale = originalScale * scale;
            yield return null;
        }
        // Fade Out Animation
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
