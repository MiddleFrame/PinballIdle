using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LetsScript : MonoBehaviour
{
    public int force;
    public Text point;
    public Text pointSum;
    Color defaultColor;
    private void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      point.text=""+(++ GameManager.Point);
        pointSum.text = NormalSum();


       GetComponent<SpriteRenderer>().color = Color.white;
        
        StartCoroutine(ChangeColor());
        collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);

        StopCoroutine(ChangeColor());

        
    }
    static public string NormalSum()
    {
        string res;
        if (GameManager.PointSum + 1 >= 1000 && GameManager.PointSum < 10000)
                res = Math.Round((++GameManager.PointSum) / 1000.0, 2, MidpointRounding.AwayFromZero)+ "K";
        else if (GameManager.PointSum + 1 >= 10000 && GameManager.PointSum < 100000)
                res = Math.Round((++GameManager.PointSum) / 1000.0, 1, MidpointRounding.AwayFromZero) + "K";
        else if (GameManager.PointSum + 1 >= 100000 && GameManager.PointSum < 1000000)
                res = (int)((++GameManager.PointSum) / 1000.0) + "K";
        else if (GameManager.PointSum + 1 >= 1000000 && GameManager.PointSum < 10000000)
                res = Math.Round((++GameManager.PointSum) / 1000000.0, 2, MidpointRounding.AwayFromZero) + "M";
        else if (GameManager.PointSum + 1 >= 10000000 && GameManager.PointSum < 100000000)
                res = Math.Round((++GameManager.PointSum) / 1000000.0, 1, MidpointRounding.AwayFromZero) + "M";
        else if (GameManager.PointSum + 1 >= 100000000 && GameManager.PointSum < 1000000000)
                res = Math.Round((++GameManager.PointSum) / 1000000.0) + "M";
        else if (GameManager.PointSum + 1 >= 1000000000 && GameManager.PointSum < 10000000000)
                res = Math.Round((++GameManager.PointSum) / 1000000000.0, 2, MidpointRounding.AwayFromZero) + "B";
        else if (GameManager.PointSum + 1 >= 10000000000 && GameManager.PointSum < 100000000000)
                res = Math.Round((++GameManager.PointSum) / 1000000000.0,1, MidpointRounding.AwayFromZero) + "B";
        else if(GameManager.PointSum + 1 >= 100000000000 )
                res = Math.Round((++GameManager.PointSum) / 1000000000.0) + "B";
        else
                res = "" +(++GameManager.PointSum); 
        return res;
    }
   public  IEnumerator ChangeColor()
    {
        
        yield return new WaitForSeconds(0.02f);
       GetComponent<SpriteRenderer>().color = defaultColor;
    }
}
