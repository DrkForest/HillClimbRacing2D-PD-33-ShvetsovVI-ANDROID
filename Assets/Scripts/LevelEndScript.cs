using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    private CarController carController;
    // Start is called before the first frame update
    void Start()
    {
        carController = GameObject.Find("Car").GetComponent<CarController>();
    }

    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.name == "Car")
        {
            Debug.Log(info);

            carController.OnGround();
            //Destroy(gameObject);
        }
        //|| info.name == "car-head" || info.name == "car-body" || info.name == "car-wheel" || info.name == "car-wheel (1)" 
    }
}
