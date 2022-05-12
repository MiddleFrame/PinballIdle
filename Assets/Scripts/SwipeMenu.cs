using Controllers;
using Managers;
using Shop;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Screen;

public class SwipeMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public AudioSource[] As;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!DefaultBuff.autoMod[FieldManager.currentField])
        {

            As[FieldManager.currentField].Play();
        }
        FlipperController.IsFlipper[FieldManager.currentField] = true;
        FlipperController.RightOrLeft[FieldManager.currentField] = eventData.position.x > width/2.0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FlipperController.IsFlipper[FieldManager.currentField] = false;
    }

   
}