using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPicker : MonoBehaviour
{
    [SerializeField] private int fuelValue;
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.name == "Car")
        {
            Debug.Log(info);
            info.GetComponent<CarController>().AddFuel(fuelValue);
            Destroy(gameObject);
        }
        //|| info.name == "car-head" || info.name == "car-body" || info.name == "car-wheel" || info.name == "car-wheel (1)" 
    }
}
