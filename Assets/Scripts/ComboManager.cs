using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ComboText comboText;
    [SerializeField] private Image comboMeter;
    [SerializeField] private RectTransform shakeTarget;

    [Header("Settings")]
    [SerializeField] private float comboResetTime = 2f;
    [SerializeField] private float maxCombo = 20f;
    [SerializeField] private float shakeAmount = 10f;
    [SerializeField] private float shakeDuration = 0.15f;

    [Header("Combo Text (per tier)")]
    [SerializeField]
    private string[] comboWords =
    {
        "Good!",
        "Great!",
        "Wonderful!",
        "Amazing!",
        "BRAH!",
        "Skrrt Skrrt!"
    };

    [SerializeField]
    private Color[] comboColors =
    {
        Color.yellow,
        new Color(1f, 0.65f, 0.2f),
        Color.red,
        new Color(0.95f, 0.4f, 0.95f)
    };

    [Header("Voice Lines (Plays In Order)")]
    [SerializeField] private AudioClip[] voiceLines;

    private AudioSource audioSource;

    private int pickupCount = 0;      // counts every receipt
    private float lastPickupTime;
    private Vector3 originalShakePos;

    private int voiceLineIndex = 0;   // ordered VO index

    // --------------------------------------------------
    // UNITY LIFECYCLE
    // --------------------------------------------------

    private void Start()
    {
        if (shakeTarget != null)
            originalShakePos = shakeTarget.localPosition;

        if (comboMeter != null)
            comboMeter.fillAmount = 0f;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        // Reset combo if time expires
        if (pickupCount > 0 && Time.time - lastPickupTime > comboResetTime)
        {
            pickupCount = 0;
            voiceLineIndex = 0;

            if (comboMeter != null)
                comboMeter.fillAmount = 0f;
        }
    }

    // --------------------------------------------------
    // COMBO LOGIC
    // --------------------------------------------------

    public void AddCombo()
    {
        pickupCount++;
        lastPickupTime = Time.time;

        // ❗ UI triggers ONLY every 3 pickups
        if (pickupCount % 3 != 0)
            return;

        int tier = (pickupCount / 3) - 1;
        tier = Mathf.Clamp(tier, 0, comboWords.Length - 1);

        string word = comboWords[tier];
        Color color = comboColors[tier % comboColors.Length];

        // Show combo UI
        if (comboText != null)
            comboText.Show(word, color);

        UpdateComboMeter();
        StartCoroutine(ShakeUI());

        // Play ordered voice line
        PlayVoiceLine();

        Debug.Log($"Combo Tier Triggered at pickup: {pickupCount}");
    }

    private void UpdateComboMeter()
    {
        if (comboMeter == null) return;

        float fill = Mathf.Clamp01(pickupCount / maxCombo);
        comboMeter.fillAmount = fill;
    }

    // --------------------------------------------------
    // UI EFFECTS
    // --------------------------------------------------

    private IEnumerator ShakeUI()
    {
        if (shakeTarget == null) yield break;

        float timer = 0f;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount, shakeAmount);

            shakeTarget.localPosition =
                originalShakePos + new Vector3(x, y, 0f);

            yield return null;
        }

        shakeTarget.localPosition = originalShakePos;
    }

    // --------------------------------------------------
    // AUDIO (ORDERED, EVERY 3 PICKUPS)
    // --------------------------------------------------

    private void PlayVoiceLine()
    {
        if (voiceLines == null || voiceLines.Length == 0)
            return;

        if (audioSource == null)
            return;

        audioSource.PlayOneShot(voiceLines[voiceLineIndex]);

        voiceLineIndex++;
        if (voiceLineIndex >= voiceLines.Length)
            voiceLineIndex = 0;
    }
}
