using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SwipeMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public AudioSource[] As;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!StandartBuff.automod[FieldManager.currentField])
        {

            As[FieldManager.currentField].Play();
        }
        FlipperController.IsFlipper[FieldManager.currentField] = true;
        if (eventData.position.x > Screen.width/2)
            FlipperController.rightorleft = true;
        else
            FlipperController.rightorleft = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FlipperController.IsFlipper[FieldManager.currentField] = false;
    }

   
}