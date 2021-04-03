using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private int coinValue;
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.name == "Car")
        {
            Debug.Log(info);
            info.GetComponent<CarController>().AddCoins(coinValue);
            Destroy(gameObject);
        }
        //|| info.name == "car-head" || info.name == "car-body" || info.name == "car-wheel" || info.name == "car-wheel (1)" 
    }
}
