using UnityEngine;
using TMPro;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text comboText;

    [Header("Settings")]
    [SerializeField] private float comboResetTime = 2f;
    [SerializeField] private string[] comboWords = { "Good!", "Great!", "Amazing!", "Skrrt Skrrt!" };
    [SerializeField]
    private Color[] comboColors = {
        Color.yellow,
        new Color(1f, 0.5f, 0f),
        Color.red,
        new Color(1f, 0f, 1f)
    };

    private int comboCount = 0;
    private float lastComboTime;
    private Coroutine comboRoutine;

    void Start()
    {
        if (comboText != null)
            comboText.text = "";
    }

    public void AddCombo()
    {
        comboCount++;
        lastComboTime = Time.time;

        int index = Mathf.Min(comboCount - 1, comboWords.Length - 1);
        comboText.text = comboWords[index];
        comboText.color = comboColors[index % comboColors.Length];

        if (comboRoutine != null)
            StopCoroutine(comboRoutine);

        comboRoutine = StartCoroutine(AnimateComboText());
    }

    private IEnumerator AnimateComboText()
    {
        comboText.alpha = 1f;
        comboText.transform.localScale = Vector3.one * 1.2f;

        float t = 0f;
        while (t < 0.25f)
        {
            t += Time.deltaTime;
            comboText.transform.localScale = Vector3.Lerp(Vector3.one * 1.2f, Vector3.one, t / 0.25f);
            yield return null;
        }

        yield return new WaitForSeconds(comboResetTime);

        comboCount = 0;
        comboText.text = "";
    }

    void Update()
    {
        if (comboCount > 0 && Time.time - lastComboTime > comboResetTime)
        {
            comboCount = 0;
            comboText.text = "";
        }
    }
}
