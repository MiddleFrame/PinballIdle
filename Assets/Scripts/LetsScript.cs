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
    public GameObject text;
  
    private void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      point.text=""+(++GameManager.Point);
        if (GameManager.Point > GameManager.maximumPoint)
            GameManager.maximumPoint = GameManager.Point;
        GameManager.PointSum+=StandartBuff.pointOnBit;
        QuestManager.Updates();
        pointSum.text = NormalSum(GameManager.PointSum);
        //Вернуться и удалить если хуйня
        if(GameManager.ExNum)
        ExperemetNumber(collision.contacts[0]);
       GetComponent<SpriteRenderer>().color = Color.white;
        
        StartCoroutine(ChangeColor());
        collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);

        StopCoroutine(ChangeColor());

        
    }
    static public string NormalSum( long p)
    {
        string res;
        if (p + 1 >= 1000 && p < 10000)
                res = Math.Round((p) / 1000.0, 2, MidpointRounding.AwayFromZero)+ "K";
        else if (p + 1 >= 10000 && p < 100000)
                res = Math.Round((p) / 1000.0, 1, MidpointRounding.AwayFromZero) + "K";
        else if (p + 1 >= 100000 && p < 1000000)
                res = (int)(p / 1000.0) + "K";
        else if (p + 1 >= 1000000 && p < 10000000)
                res = Math.Round(p / 1000000.0, 2, MidpointRounding.AwayFromZero) + "M";
        else if (p + 1 >= 10000000 && p< 100000000)
                res = Math.Round(p / 1000000.0, 1, MidpointRounding.AwayFromZero) + "M";
        else if (p + 1 >= 100000000 && p < 1000000000)
                res = Math.Round(p / 1000000.0) + "M";
        else if (p + 1 >= 1000000000 && p< 10000000000)
                res = Math.Round(p / 1000000000.0, 2, MidpointRounding.AwayFromZero) + "B";
        else if (p + 1 >= 10000000000 && p < 100000000000)
                res = Math.Round(p / 1000000000.0,1, MidpointRounding.AwayFromZero) + "B";
        else if(p + 1 >= 100000000000 )
                res = Math.Round(p / 1000000000.0) + "B";
        else
                res = "" +(p); 
        return res;
    }
   public  IEnumerator ChangeColor()
    {
        
        yield return new WaitForSeconds(0.02f);
        GetComponent<SpriteRenderer>().color = defaultColor;
    }


    public void ExperemetNumber(ContactPoint2D cp2d)
    {
       Instantiate(text, new Vector2(cp2d.point.x, cp2d.point.y), new Quaternion());
        
    }

   
}
