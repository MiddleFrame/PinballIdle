using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static int[] CostFields = new int[] { 10 };
    public GameManager Gm;
    public GameObject textError;
    public GameObject NewFieldPanel;
    public GameObject[] ButtonsToBuy;
    public static bool[] Fields = new bool[] { false, false,false };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void BuyNewField(int NumbeField)
    {
        if (GameManager.gems >= CostFields[NumbeField])
        {
            _buyNewField(NumbeField);
            GameManager.gems -= CostFields[NumbeField];
            NewFieldPanel.SetActive(true);
        }
        else
        {
            textError.SetActive(true);
            StartCoroutine(Gm.ViewText(textError));
            StopCoroutine(Gm.ViewText(textError));
        }



    }
    public void _buyNewField(int NumbeField)
    {
        ButtonsToBuy[NumbeField].SetActive(false);
        Fields[NumbeField] = true;
        if (!Gm.Arrows[0].activeSelf)
        {
            Gm.Arrows[0].SetActive(true);
            //Временная мреа
            Gm.Arrows[1].SetActive(true);
        }
    }
}
