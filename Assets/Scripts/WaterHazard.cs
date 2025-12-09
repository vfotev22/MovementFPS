using UnityEngine;

public class WaterHazardTrigger : MonoBehaviour
{
    public float damageCooldown = 3f;
    private float timer = 0f;

    [Header("Audio Settings")]
    public AudioClip waterSound;
    private AudioSource audioSource;
    private bool hasPlayedSound = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!hasPlayedSound && waterSound != null)
        {
            audioSource.PlayOneShot(waterSound);
            hasPlayedSound = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        Timer t = FindObjectOfType<Timer>();
        t.AddTime(-3f);
        t.SetTimerMultiplier(0.10f);

        timer = damageCooldown;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 0f;
            hasPlayedSound = false;
        }
    }
}
