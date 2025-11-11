using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TMP_Text timerText;

    public GameObject gameOverCanvas;
    public MonoBehaviour[] playerControlScripts;
    private bool isRunning = true;
    private bool didGameOver = false;

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
            UpdateTimerDisplay();
        }
    }

    void Update()
    {
        if (!isRunning)
        {
            return;
        }

        timeRemaining -= Time.unscaledDeltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
            TriggerGameOver();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = $"Timer: {minutes:0}:{seconds:00}";
    }

    void TriggerGameOver()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}
