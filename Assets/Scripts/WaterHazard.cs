using UnityEngine;

public class WaterHazardTrigger : MonoBehaviour
{
    public float damageCooldown = 3f;
    private float timer = 0f;

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
        }
    }
}
