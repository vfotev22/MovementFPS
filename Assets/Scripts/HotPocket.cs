using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPocket : MonoBehaviour
{
    //public GameObject timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Timer>().AddTime(6f);
            FindObjectOfType<Timer>().SetTimerMultiplier(-0.10f);
                
            Destroy(gameObject);
        }
    }
}