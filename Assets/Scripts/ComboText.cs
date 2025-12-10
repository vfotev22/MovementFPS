using System.Collections;
using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_Text text;

    [Header("Animation Settings")]
    [SerializeField] private float popScale = 1.3f;
    [SerializeField] private float popDuration = 0.25f;
    [SerializeField] private float fadeDuration = 0.75f;

    private Vector3 originalScale;
    private Coroutine currentRoutine;

    [Header("Voice Lines")]
    public AudioClip[] voiceLines;
    private AudioSource audioSource;

    void Awake()
    {
        // Assign automatically IF the inspector reference is missing
        if (text == null)
            text = GetComponent<TMP_Text>();

        originalScale = transform.localScale;
        text.alpha = 0f;

        audioSource = GetComponent<AudioSource>();
    }

    public void Show(string message, Color color)
    {
        text.text = message;
        text.color = color;

        // Stop old animation if one is playing
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        // Start the new pop + fade animation
        currentRoutine = StartCoroutine(AnimateText());

        PlayVoiceLine();
    }

    private IEnumerator AnimateText()
    {
        // Make visible & pop big
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

        // Pause briefly
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

    private void PlayVoiceLine()
    {
        if (voiceLines == null || voiceLines.Length == 0)
            return;

        if (audioSource == null)
            return;

        int index = Random.Range(0, voiceLines.Length);
        audioSource.PlayOneShot(voiceLines[index]);
    }
}
