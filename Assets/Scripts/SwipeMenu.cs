using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SwipeMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public float posdown;
    public AudioSource[] As;
    public AudioClip Ac;
    public GameObject[] Fields;
    public GameObject[] Points;
    public GameObject[] PointsNow;
    public AudioSource[] LetSourse;
    public AudioSource Checker;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.automod[FieldManager.CorrectField])
        {

            As[FieldManager.CorrectField].PlayOneShot(Ac);
        }
        FlipperController.IsFlipper[FieldManager.CorrectField] = true;
        posdown = eventData.position.x;
        if (eventData.position.x > Screen.width/2)
            FlipperController.rightorleft = true;
        else
            FlipperController.rightorleft = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FlipperController.IsFlipper[FieldManager.CorrectField] = false;
       /* if (!isSwapped)
        {
            
            if ((eventData.position.x - posdown) < -200f && FieldManager.CorrectField < FieldManager.Fields.Length && FieldManager.Fields[FieldManager.CorrectField + 1])
            {
                StartCoroutine(Swiped(true));
                FieldManager.CorrectField++;
                Points[FieldManager.CorrectField].SetActive(true);
                PointsNow[FieldManager.CorrectField].SetActive(true);
                for (int i = 0; i < Points.Length; i++)
                {
                    if (i != FieldManager.CorrectField)
                        Points[i].SetActive(false);
                }
                for (int i = 0; i < PointsNow.Length; i++)
                {
                    if (i != FieldManager.CorrectField)
                        PointsNow[i].SetActive(false);
                }
            }
            else if ((eventData.position.x - posdown) > 200f && FieldManager.CorrectField > 0)
            {
                StartCoroutine(Swiped(false));
                FieldManager.CorrectField--;
                Points[FieldManager.CorrectField].SetActive(true);

                for (int i = 0; i < Points.Length; i++)
                {
                    if (i != FieldManager.CorrectField)
                        Points[i].SetActive(false);
                }
                PointsNow[FieldManager.CorrectField].SetActive(true);
                for (int i = 0; i < PointsNow.Length; i++)
                {
                    if (i != FieldManager.CorrectField)
                        PointsNow[i].SetActive(false);
                }
            }
            for (int i = 0; i < As.Length; i++)
            {
                if (i != FieldManager.CorrectField)
                {
                    As[i].volume = 0;
                    LetSourse[i].volume = 0;
                }
                else
                {
                    As[i].volume = 1;
                    LetSourse[i].volume = 1;
                }
            }
        }*/
    }

    public void SwipeRight()
    {
        FlipperController.IsFlipper[FieldManager.CorrectField] = false;
        if (!isSwapped && FieldManager.CorrectField < FieldManager.Fields.Length && FieldManager.Fields[FieldManager.CorrectField + 1] )
        {
            StartCoroutine(Swiped(true));
            FieldManager.CorrectField++;
            Points[FieldManager.CorrectField].SetActive(true);
            PointsNow[FieldManager.CorrectField].SetActive(true);
            for (int i = 0; i < Points.Length; i++)
            {
                if (i != FieldManager.CorrectField)
                    Points[i].SetActive(false);
            }
            for (int i = 0; i < PointsNow.Length; i++)
            {
                if (i != FieldManager.CorrectField)
                    PointsNow[i].SetActive(false);
            }
        }
        for (int i = 0; i < As.Length; i++)
        {
            if (i != FieldManager.CorrectField)
            {
                As[i].volume = 0;
                LetSourse[i].volume = 0;
                Checker.volume = 0;
            }
            else
            {
                As[i].volume = 1;
                LetSourse[i].volume = 1;
            }
            
        }
    }
    public void SwipeLeft()
    {
        FlipperController.IsFlipper[FieldManager.CorrectField] = false;
        if (!isSwapped && FieldManager.CorrectField > 0)
        {
            StartCoroutine(Swiped(false));
            FieldManager.CorrectField--;
            Points[FieldManager.CorrectField].SetActive(true);

            for (int i = 0; i < Points.Length; i++)
            {
                if (i != FieldManager.CorrectField)
                    Points[i].SetActive(false);
            }
            PointsNow[FieldManager.CorrectField].SetActive(true);
            for (int i = 0; i < PointsNow.Length; i++)
            {
                if (i != FieldManager.CorrectField)
                    PointsNow[i].SetActive(false);
            }
        }
        for (int i = 0; i < As.Length; i++)
        {
            if (i != FieldManager.CorrectField)
            {
                As[i].volume = 0;
                LetSourse[i].volume = 0;
            }
            else
            {
                if (i == 0)
                    Checker.volume = 1;
                As[i].volume = 1;
                LetSourse[i].volume = 1;
            }
        }
    }
    bool isSwapped=false;
    IEnumerator Swiped(bool left)
    {
        isSwapped = true;
        var x = Fields[0].transform.localPosition.x;
        if(left)
        while (Fields[0].transform.localPosition.x > x - 720)
            { 

            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i].transform.localPosition -= new Vector3(20f, 0, 0);
            }
            yield return new WaitForEndOfFrame();
        }
        else
            while (Fields[0].transform.localPosition.x < x + 700)
            {

                for (int i = 0; i < Fields.Length; i++)
                {
                    Fields[i].transform.localPosition += new Vector3(20f, 0, 0);
                }
                yield return new WaitForEndOfFrame();
            }
        isSwapped = false;
    }
}