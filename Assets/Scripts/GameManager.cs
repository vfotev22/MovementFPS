using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class GameUIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TMP_Text scoreText;

    [Header("End Screens")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text winScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button winRetryButton;



    private int score;
    private bool ended;
    public int Score => score;

    void Awake()
    {
        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(Retry);
        }
        if(winRetryButton != null)
        {
            winRetryButton.onClick.AddListener(Retry);
        }        
    }

    void Start()
    {
        UpdateScoreLabel();
    }

    public void AddScore(int amount)
    {
        if (ended) return;
        score += amount;
        UpdateScoreLabel();
    }

    public void ShowGameOver()
    {
        if (ended) return;
        ended = true;
        gameOverScoreText.text = $"Score: {score}/20";
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if(retryButton)
        {
            EventSystem.current.SetSelectedGameObject(retryButton.gameObject);
        }
    }

    public void ShowWin()
    {
        if (ended) return;
        ended = true;
        winScoreText.text = $"Score: {score}/20";
        winPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Retry()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("Test Course");
    }

    private void UpdateScoreLabel()
    {
        if (scoreText) scoreText.text = $"Score: {score}/20";
    }


    public IEnumerator RestartWithFade()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartGame()
    {
        // Ensure time is normal again (if game was paused)
        Time.timeScale = 1f;

        // Reload the active scene to reset everything
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
