using UnityEngine;
using TMPro;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private comboText comboText;

    [Header("Settings")]
    [SerializeField] private float comboResetTime = 2f;
    [SerializeField] private string[] comboWords = { "Good!", "Great!", "Amazing!", "Skrrt Skrrt!" };
    [SerializeField]
    private Color[] comboColors = {
        Color.yellow,//for "good" yellow color
        new Color(1f, 0.65f, 0.2f), //for "great" orange color
        Color.red,//for "amazing" red color
        new Color(0.95f, 0.4f, .95f)//for "skrrt skrrt" Magenta color
    };

    private int comboCount = 0;
    private float lastComboTime;

    public void AddCombo()
    {
        comboCount++;
        lastComboTime = Time.time;

        int index = Mathf.Min(comboCount - 1, comboWords.Length - 1);
        string word = comboWords[index];
        Color color = comboColors[Mathf.Min(index % comboColors.Length)];

        if (comboText)
            comboText.ShowCombo(word, color);
    }

    void Update()
    {
        if (comboCount > 0 && Time.time - lastComboTime > comboResetTime)
        {
            comboCount = 0;
            
        }
    }
}
