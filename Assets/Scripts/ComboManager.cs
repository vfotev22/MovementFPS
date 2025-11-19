using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ComboText comboText;
    [SerializeField] private Image comboMeter;   // Combo bar reference
    [SerializeField] private RectTransform shakeTarget; // UI object to shake

    [Header("Settings")]
    [SerializeField] private float comboResetTime = 2f;
    [SerializeField] private float maxCombo = 20f; // meter max value
    [SerializeField] private float shakeAmount = 10f;
    [SerializeField] private float shakeDuration = 0.15f;

    [SerializeField] private string[] comboWords = { "Good!", "Great!", "Wonderful!", "Amazing!", "BRAH!","Skrrt Skrrt!" };
    [SerializeField]
    private Color[] comboColors = {
        Color.yellow,
        new Color(1f, 0.65f, 0.2f),
        Color.red,
        new Color(0.95f, 0.4f, 0.95f)
    };

    private int comboCount = 0;
    private float lastComboTime;
    private Vector3 originalShakePos;

    void Start()
    {
        originalShakePos = shakeTarget.localPosition;

        if (comboMeter != null)
            comboMeter.fillAmount = 0f;
    }

    public void AddCombo()
    {
        comboCount++;
        lastComboTime = Time.time;

        int index = Mathf.Min(comboCount - 1, comboWords.Length - 1);
        string word = comboWords[index];
        Color color = comboColors[index % comboColors.Length];

        comboText?.Show(word, color);

        UpdateComboMeter();
        StartCoroutine(ShakeUI());

        Debug.Log($"Combo triggered: {word}");
    }

    private void UpdateComboMeter()
    {
        if (comboMeter == null) return;

        float fill = Mathf.Clamp01(comboCount / maxCombo);
        comboMeter.fillAmount = fill;
    }

    private IEnumerator ShakeUI()
    {
        float timer = 0f;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount, shakeAmount);

            shakeTarget.localPosition = originalShakePos + new Vector3(x, y, 0);

            yield return null;
        }

        shakeTarget.localPosition = originalShakePos;
    }

    private void Update()
    {
        if (comboCount > 0 && Time.time - lastComboTime > comboResetTime)
        {
            comboCount = 0;

            if (comboMeter != null)
                comboMeter.fillAmount = 0f;
        }
    }
}