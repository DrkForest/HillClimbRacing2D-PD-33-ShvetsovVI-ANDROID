using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameClickController : MonoBehaviour
{

    protected CarController CarController;

    [SerializeField] protected Button _gas;
    [SerializeField] protected Button _brake;

    [SerializeField] protected Image _gas_img;
    [SerializeField] protected Image _brake_img;

    public bool _ControllButtonClick;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    protected virtual void OnDestroy()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CarOnClickGas()
    {

    }
    private void CarOnClickBrake()
    {

    }
}
