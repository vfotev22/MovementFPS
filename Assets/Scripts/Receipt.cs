using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt : MonoBehaviour
{
    [Header("Money Settings")]
    public float moneyValue = 1.00f;   

    [Header("Reference")]
    public MoneyUI moneyUI;            

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (moneyUI != null)
                moneyUI.AddMoney(moneyValue);
                
            Destroy(gameObject);
        }
    }
}
