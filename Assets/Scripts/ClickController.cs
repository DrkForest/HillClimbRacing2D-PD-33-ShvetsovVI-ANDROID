using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickController : EventTrigger
{
    //
    public bool IsClicked = false;

    
    // Start is called before the first frame update
    public override void OnPointerDown(PointerEventData data)
    {
        IsClicked = true;
        

    }

    public override void OnPointerUp(PointerEventData data)
    {
        IsClicked = false;
        
    }
}
