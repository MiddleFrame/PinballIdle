using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBall : MonoBehaviour
{
    public GameObject[] ChoosenImage;
    public void ChooseBalls(int i)
    {
        
        var child = Teleport.mainballsstatic[0].GetComponentsInChildren<TrailRenderer>();
        GameManager.choosenBall = i;
        for(int j=0; j < ChoosenImage.Length; j++)
        {
            if (j != i)
                ChoosenImage[j].SetActive(false);

            if (j == i)
            {
                child[j].enabled = (true);
            }
            else
            {
                child[j].enabled = (false);
            }               
            
           
        }
        ChoosenImage[i].SetActive(true);
        Teleport.mainballsstatic[0].GetComponent<SpriteRenderer>().color = GameManager.instance.colorsBall[i];
        

    }
}
