using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseClick : EventTrigger
{
    //
    

    public bool PauseIsClicked = false;

    public override void OnPointerDown(PointerEventData data)
    {
        PauseIsClicked = true;
        Debug.Log("Pause");
    }

    public override void OnPointerUp(PointerEventData data)
    {
        PauseIsClicked = false;
       
    }
}
