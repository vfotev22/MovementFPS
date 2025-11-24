using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHazard : MonoBehaviour
{
    public float TimeUntilNextTouch = 3.00f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && TimeUntilNextTouch <= 0)
        {
            FindObjectOfType<Timer>().AddTime(-3f);
            FindObjectOfType<Timer>().SetTimerMultiplier(0.10f);
                
            Destroy(gameObject);
        }
    }

    void Update(){
        if (TimeUntilNextTouch > 0) {TimeUntilNextTouch -= Time.deltaTime;}
    }
}

