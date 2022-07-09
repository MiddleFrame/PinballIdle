using Competition;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Screen;

public class SwipeMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public AudioSource[] As;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (FieldManager.currentField>=0)
        {
            As[FieldManager.currentField].Play();
        }

        if (LetsScript.isCompetitive)
        {
            FlipperCompetition.IsFlipper[FieldManager.currentField] = true;
            FlipperCompetition.Left[FieldManager.currentField] = eventData.position.x <= width/2.0;
            FlipperCompetition.Right[FieldManager.currentField] = eventData.position.x > width/2.0;
            return;
        }
        FlipperController.IsFlipper[FieldManager.currentField] = true;
        FlipperController.RightOrLeft[FieldManager.currentField] = eventData.position.x > width/2.0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (LetsScript.isCompetitive)
        {
            FlipperCompetition.IsFlipper[FieldManager.currentField] = false;
            FlipperCompetition.Right[FieldManager.currentField] = false;
            FlipperCompetition.Left[FieldManager.currentField] = false;
        }

        FlipperController.IsFlipper[FieldManager.currentField] = false;
    }

   
}