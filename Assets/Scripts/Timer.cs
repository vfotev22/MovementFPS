using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeRemaining = 60f;
    public float maxTime = 60f;       // Easier to change later
    private bool isRunning = true;
    private bool didGameOver = false;

    [Header("UI Text")]
    public TMP_Text timerText;

    [Header("UI Bar (Fill)")]
    public Image timerFillImage;
    public RectTransform barTransform;

    [Header("Color Settings")]
    public Color highColor = Color.green;
    public Color midColor = Color.yellow;
    public Color lowColor = Color.red;

    [Header("Pulse Effect")]
    public float pulseThreshold = 0.25f; // 25% of time left
    public float pulseSpeed = 6f;
    public float pulseAmount = 0.1f;

    private Vector3 originalScale;
    private bool isPulsing = false;

    [Header("Game Over UI")]
    public GameObject gameOverCanvas;
    public MonoBehaviour[] playerControlScripts;

    void Start()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        originalScale = barTransform.localScale;

        UpdateTimerDisplay();
        UpdateTimerBar();
        UpdateColor(1f);
    }

    void Update()
    {
        if (!isRunning || didGameOver)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            TriggerGameOver();
        }

        float normalized = timeRemaining / maxTime;

        UpdateTimerDisplay();
        UpdateTimerBar();
        UpdateColor(normalized);
        HandlePulse(normalized);
    }

    // ---------------- TIMER UI TEXT -----------------
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = $"Timer: {minutes:0}:{seconds:00}";
    }

    // ---------------- FILL BAR -----------------
    void UpdateTimerBar()
    {
        if (timerFillImage != null)
        {
            timerFillImage.fillAmount = timeRemaining / maxTime;
        }
    }

    // ---------------- COLOR TRANSITIONS -----------------
    void UpdateColor(float normalized)
    {
        if (timerFillImage == null)
            return;

        if (normalized > 0.6f)    // High (Green)
        {
            timerFillImage.color = Color.Lerp(timerFillImage.color, highColor, 8f * Time.deltaTime);
        }
        else if (normalized > 0.3f) // Mid (Yellow)
        {
            timerFillImage.color = Color.Lerp(timerFillImage.color, midColor, 8f * Time.deltaTime);
        }
        else   // Low (Red)
        {
            timerFillImage.color = Color.Lerp(timerFillImage.color, lowColor, 8f * Time.deltaTime);
        }
    }

    // ---------------- PULSE EFFECT -----------------
    void HandlePulse(float normalized)
    {
        if (barTransform == null)
            return;

        if (normalized <= pulseThreshold)
        {
            isPulsing = true;
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            barTransform.localScale = new Vector3(pulse, pulse, 1);
        }
        else
        {
            if (isPulsing)
            {
                barTransform.localScale = originalScale;
                isPulsing = false;
            }
        }
    }

    // ---------------- GAME OVER -----------------
    void TriggerGameOver()
    {
        if (didGameOver)
            return;

        didGameOver = true;
        isRunning = false;

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;

        foreach (var script in playerControlScripts)
        {
            if (script)
                script.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ---------------- MANUAL CONTROLS -----------------
    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;

    public void AddTime(float amount)
    {
        timeRemaining += amount;
        if (timeRemaining > maxTime)
            timeRemaining = maxTime;
    }
}
