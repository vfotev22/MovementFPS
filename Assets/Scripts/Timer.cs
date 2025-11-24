using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer Speed")]
    public float TimeMultiplier = 1f;

    [Header("Timer Settings")]
    public float timeRemaining = 60f;
    public float maxTime = 60f;
    private bool isRunning = true;
    private bool didGameOver = false;

    [Header("UI Bar (Fill)")]
    public Image timerFillImage;          // The bar that shrinks
    public RectTransform barTransform;    // Used for pulse effect

    [Header("Color Settings")]
    public Color highColor = Color.green;
    public Color midColor = Color.yellow;
    public Color lowColor = Color.red;

    [Header("Pulse Effect")]
    public float pulseThreshold = 0.25f;
    public float pulseSpeed = 6f;
    public float pulseAmount = 0.1f;

    private Vector3 originalScale;
    private bool isPulsing = false;

    [Header("Game Over UI Handler")]
    public GameUIManager uiManager;

    void Start()
    {
        originalScale = barTransform.localScale;

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

        UpdateTimerBar();
        UpdateColor(normalized);
        HandlePulse(normalized);
    }

    // ---------------- FILL BAR -----------------
    void UpdateTimerBar()
    {
        if (timerFillImage != null)
            timerFillImage.fillAmount = timeRemaining / maxTime;
    }

    // ---------------- COLOR TRANSITIONS -----------------
    void UpdateColor(float normalized)
    {
        if (timerFillImage == null) return;

        if (normalized > 0.6f)
            timerFillImage.color = Color.Lerp(timerFillImage.color, highColor, 8f * Time.deltaTime);
        else if (normalized > 0.3f)
            timerFillImage.color = Color.Lerp(timerFillImage.color, midColor, 8f * Time.deltaTime);
        else
            timerFillImage.color = Color.Lerp(timerFillImage.color, lowColor, 8f * Time.deltaTime);
    }

    // ---------------- PULSE EFFECT -----------------
    void HandlePulse(float normalized)
    {
        if (barTransform == null) return;

        if (normalized <= pulseThreshold)
        {
            isPulsing = true;
            float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            barTransform.localScale = new Vector3(pulse, pulse, 1);
        }
        else if (isPulsing)
        {
            barTransform.localScale = originalScale;
            isPulsing = false;
        }
    }

    // ---------------- GAME OVER -----------------
    void TriggerGameOver()
    {
        if (didGameOver) return;

        didGameOver = true;
        isRunning = false;

        if (uiManager != null)
            uiManager.ShowGameOver();       // ⭐ Clean direct reference
        else
            Debug.LogError("Timer Error: uiManager is NULL — drag GameUIManager into the Timer!");
    }

    // ---------------- PUBLIC METHODS -----------------
    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;

    public void AddTime(float amount)
    {
        timeRemaining += amount;
        if (timeRemaining > maxTime)
            timeRemaining = maxTime;

        UpdateTimerBar(); // update bar instantly
    }

    public void SetTimerMultiplier(float amount)
    {
        TimeMultiplier += amount;
        if (TimeMultiplier >= 1.50) { TimeMultiplier = 1.50f; }
        if (TimeMultiplier <= 0.90) { TimeMultiplier = 0.90f; }
    }
}
