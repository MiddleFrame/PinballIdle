using System;
using Competition;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Screen;

public class SwipeMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static SwipeMenu instance;
    public AudioSource[] As;

    private void Awake()
    {
        As = new AudioSource[9];
        instance = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (LetsScript.isCompetitive)
        {
            FlipperCompetition.IsFlipper[FieldManager.currentField] = true;
            FlipperCompetition.Left[FieldManager.currentField] = eventData.position.x <= width / 2.0;
            FlipperCompetition.Right[FieldManager.currentField] = eventData.position.x > width / 2.0;
            return;
        }


        if (ChallengeManager.IsStartChallenge[FieldManager.currentField])
        {
            ChallengeManager.progress.currentProgressChallenge[FieldManager.currentField]++;
            ChallengeManager.Instance.ChangeTextAndFill(FieldManager.currentField);
        }

        if (NewElement.isBallInElement)
        {
            int point = (int) (Shop.DefaultBuff.grade.pointOnBit[FieldManager.currentField] *
                               Shop.DefaultBuff.grade.multiply[FieldManager.currentField]);
            PlayerDataController.PointSum += point;
            LetsScript.ShowNumber(transform.position, point.ToString());
            return;
        }

        if (Shop.DefaultBuff.autoMod[FieldManager.currentField]) return;
        FlipperController.IsFlipper[FieldManager.currentField] = true;
        FlipperController.RightOrLeft[FieldManager.currentField] = eventData.position.x > width / 2.0;
        if (FieldManager.currentField >= 0)
        {
            As[FieldManager.currentField].Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (LetsScript.isCompetitive)
        {
            FlipperCompetition.IsFlipper[FieldManager.currentField] = false;
            FlipperCompetition.Right[FieldManager.currentField] = false;
            FlipperCompetition.Left[FieldManager.currentField] = false;
            return;
        }

        if (Shop.DefaultBuff.autoMod[FieldManager.currentField]) return;
        FlipperController.IsFlipper[FieldManager.currentField] = false;
    }
}