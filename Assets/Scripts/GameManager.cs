using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class GameUIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TMP_Text scoreText;

    [Header("End Screens")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private Button retryButton;



    private int score;
    private bool ended;

    void Awake()
    {
        if (winPanel) winPanel.SetActive(false);
        if (gameOverPanel) gameOverPanel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(Retry);
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
        finalScoreText.text = $"Score: {score}";
        gameOverPanel.SetActive(true);
    }

    public void ShowWin()
    {
        if (ended) return;
        ended = true;
        finalScoreText.text = $"Score: {score}";
        winPanel.SetActive(true);
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateScoreLabel()
    {
        if (scoreText) scoreText.text = $"Score: {score}";
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
