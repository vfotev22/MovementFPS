using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthFill;
    [SerializeField] private TMP_Text healthText;
    private float maxHealth = 100f;
    private float currentHealth = 100f;

    [Header("Alert Meter")]
    [SerializeField] private Image alertFill;
    [SerializeField] private TMP_Text alertText;
    private float maxAlert = 100f;
    private float currentAlert = 0f;

    [Header("Combo Counter")]
    [SerializeField] private TMP_Text comboText;
    private int comboCount = 0;
    private float comboResetTime = 2f;
    private float comboTimer = 0f;

    void Start()
    {
        UpdateHealthUI();
        UpdateAlertUI();
        UpdateComboUI();
    }

    void Update()
    {
        // Auto-decay combo after timeout
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                comboCount = 0;
                UpdateComboUI();
            }
        }
    }

    // ---------- HEALTH ----------
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHealthUI();
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthFill) healthFill.fillAmount = currentHealth / maxHealth;
        if (healthText) healthText.text = $"{Mathf.RoundToInt(currentHealth)} / {Mathf.RoundToInt(maxHealth)}";
    }

    // ---------- ALERT ----------
    public void SetAlert(float value)
    {
        currentAlert = Mathf.Clamp(value, 0, maxAlert);
        UpdateAlertUI();
    }

    private void UpdateAlertUI()
    {
        if (alertFill) alertFill.fillAmount = currentAlert / maxAlert;
        if (alertText) alertText.text = $"{Mathf.RoundToInt(currentAlert)}%";
    }

    // ---------- COMBO ----------
    public void AddCombo()
    {
        comboCount++;
        comboTimer = comboResetTime;
        UpdateComboUI();
    }

    private void UpdateComboUI()
    {
        if (comboText)
        {
            if (comboCount <= 1)
                comboText.text = "";
            else
                comboText.text = $"Combo x{comboCount}";
        }
    }
}
