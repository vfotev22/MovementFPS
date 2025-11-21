using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseOnStart : MonoBehaviour
{
    public GameObject overlayCanvas;

    private bool isPaused;
    
    public Timer timerScript;

    void Start()
    {
        Pause(true);
    }

    void Update()
    {
        if (!isPaused) return;

        if (Input.GetMouseButtonDown(0))
        {
            Pause(false);
        }
    }

    void Pause(bool pause)
    {
        isPaused = pause;

        if (overlayCanvas != null)
            overlayCanvas.SetActive(pause);

        Time.timeScale = pause ? 0f : 1f;

        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pause;
        
        if (pause)
            timerScript.PauseTimer();
        else
            timerScript.ResumeTimer();
    }
}
