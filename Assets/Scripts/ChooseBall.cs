using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBall : MonoBehaviour
{
    public GameObject[] ChoosenImage;
    public GameManager Gm;
    public void ChooseBalls(int i)
    {
        var child = Gm.Balls[0].GetComponentsInChildren<TrailRenderer>();
        var childF2 = Gm.BallsF2[0].GetComponentsInChildren<TrailRenderer>();
        GameManager.choosenBall = i;
        for(int j=0; j < ChoosenImage.Length; j++)
        {
            if (j != i)
                ChoosenImage[j].SetActive(false);

            if (j == i)
            {
                child[j].enabled = (true);
                childF2[j].enabled = (true);
            }
            else
            {
                childF2[j].enabled = (false);
                child[j].enabled = (false);
            }               
            
           
        }
        ChoosenImage[i].SetActive(true);
        Gm.Balls[0].GetComponent<SpriteRenderer>().color = Gm.colorsBall[i];
        Gm.BallsF2[0].GetComponent<SpriteRenderer>().color = Gm.colorsBall[i];
        

    }
}
