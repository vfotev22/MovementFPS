using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPausePopup : MonoBehaviour
{
    [Header("UI to show when triggered")]
    public GameObject overlayCanvas;

    [Header("Timer to pause/resume")]
    public Timer timerScript;   

    [Header("Who can trigger it")]
    public string playerTag = "Player";

    private bool showing;

    void OnTriggerEnter(Collider other)
    {
        if (showing) return;
        if (!other.CompareTag(playerTag)) return;

        ShowAndPause();
    }

    void Update()
    {
        if (!showing) return;

        if (Input.GetMouseButtonDown(0))
        {
            HideAndResume();
        }
    }

    private void ShowAndPause()
    {
        showing = true;

        GetComponent<Collider>().enabled = false;

        if (overlayCanvas) overlayCanvas.SetActive(true);

        Time.timeScale = 0f;

        if (timerScript) timerScript.PauseTimer();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
    }

    private void HideAndResume()
    {
        showing = false;
        
        if (overlayCanvas) overlayCanvas.SetActive(false);

        Time.timeScale = 1f;

        if (timerScript) timerScript.ResumeTimer();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }
}
