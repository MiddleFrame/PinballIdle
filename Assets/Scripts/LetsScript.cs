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
    public AudioSource As;
    public AudioClip Ac;
    private void Start()
    {
        defaultColor = GetComponent<SpriteRenderer>().color;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        As.PlayOneShot(Ac);
      point.text=""+(++GameManager.Point);
        if (GameManager.Point > GameManager.maximumPoint)
            GameManager.maximumPoint = GameManager.Point;
        GameManager.PointSum+=StandartBuff.pointOnBit;
        QuestManager.Updates();
        pointSum.text = GameManager.NormalSum(GameManager.PointSum);
        //Вернуться и удалить если хуйня
        if(GameManager.ExNum)
        ExperemetNumber(collision.contacts[0]);
       GetComponent<SpriteRenderer>().color = Color.white;
        
        StartCoroutine(ChangeColor());
        collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);

     

        
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
