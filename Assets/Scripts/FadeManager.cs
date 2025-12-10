using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 1f;

    private void Start()
    {
        // Start from black ONLY if needed
        fadeCanvas.alpha = 1;
        StartCoroutine(FadeIn());
    }

    public void FadeOutToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeOut(string targetScene)
{
    float t = 0f;

    while (t < fadeDuration)
    {
        t += Time.unscaledDeltaTime;
        fadeCanvas.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
        yield return null;
    }

    yield return new WaitForEndOfFrame();

    SceneManager.LoadScene(targetScene);
    
    Time.timeScale = 1f;
}

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }
    }
}
