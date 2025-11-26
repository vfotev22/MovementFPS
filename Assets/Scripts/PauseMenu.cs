using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    [Header("UI")]
    public GameObject pauseCanvas;
    public Animator animator;

    [Header("Disable On Pause")]
    public MonoBehaviour[] scriptsToDisable; 

    private void Start()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);

        if (animator != null)
            animator.SetTrigger("FadeIn");

        Time.timeScale = 0f;
        AudioListener.pause = true;
        IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (var script in scriptsToDisable)
            script.enabled = false;
    }

    public void ResumeGame()
    {
        if (animator != null)
            animator.SetTrigger("FadeOut");

        StartCoroutine(UnpauseAfterAnimation());
    }

    private System.Collections.IEnumerator UnpauseAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        IsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        foreach (var script in scriptsToDisable)
            script.enabled = true;
    }

    public void RestartGame()
    {
    AudioListener.pause = false;       
    IsPaused = false;                  
    Time.timeScale = 1f;               
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen");
    }
}
