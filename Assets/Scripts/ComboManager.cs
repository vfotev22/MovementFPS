using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ComboText comboText;  //  Match class name exactly

    [Header("Settings")]
    [SerializeField] private float comboResetTime = 2f;
    [SerializeField] private string[] comboWords = { "Good!", "Great!", "Amazing!", "Skrrt Skrrt!" };
    [SerializeField]
    private Color[] comboColors = {
        Color.yellow,                     // "Good!"  Yellow
        new Color(1f, 0.65f, 0.2f),       // "Great!" Orange
        Color.red,                        // "Amazing!" Red
        new Color(0.95f, 0.4f, 0.95f)     // "Skrrt Skrrt!"  Magenta
    };

    private int comboCount = 0;
    private float lastComboTime;

    public void AddCombo()
    {
        comboCount++;
        lastComboTime = Time.time;

        int index = Mathf.Min(comboCount - 1, comboWords.Length - 1);
        string word = comboWords[index];
        Color color = comboColors[index % comboColors.Length];

        if (comboText != null)
        {
            comboText.Show(word, color); // Correct method name
            Debug.Log($"Combo triggered: {word}");
        }
        else
        {
            Debug.LogWarning("ComboText reference missing in ComboManager!");
        }
    }

    private void Update()
    {
        if (comboCount > 0 && Time.time - lastComboTime > comboResetTime)
        {
            comboCount = 0;
        }
    }
}
