using System.Collections;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Text moneyText;

    [Header("Pulse Settings")]
    public float pulseScale = 1.2f;     
    public float pulseSpeed = 0.15f;    

    private Vector3 originalScale;
    private Coroutine pulseRoutine;

    public float currentMoney = 0f;

    void Start()
    {
        originalScale = moneyText.transform.localScale;
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        moneyText.text = $"${currentMoney:0.00}";

        if (pulseRoutine != null)
            StopCoroutine(pulseRoutine);

        pulseRoutine = StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / pulseSpeed;
            moneyText.transform.localScale = Vector3.Lerp(
                originalScale,
                originalScale * pulseScale,
                t
            );
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / pulseSpeed;
            moneyText.transform.localScale = Vector3.Lerp(
                originalScale * pulseScale,
                originalScale,
                t
            );
            yield return null;
        }

        moneyText.transform.localScale = originalScale;
    }
}
