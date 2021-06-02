using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetsScript : MonoBehaviour
{
    public int force;
    public Text point;
    public Text pointSum;

    private void OnCollisionEnter2D(Collision2D collision)
    {
      point.text=""+(++ GameManager.Point);
        pointSum.text = "" +(++GameManager.PointSum);
       GetComponent<SpriteRenderer>().color = Color.yellow;
        
        StartCoroutine(ChangeColor());
        collision.rigidbody.AddForce(-collision.contacts[0].normal * force, ForceMode2D.Impulse);

        StopCoroutine(ChangeColor());


    }

   public  IEnumerator ChangeColor()
    {
        
        yield return new WaitForSeconds(0.05f);
       GetComponent<SpriteRenderer>().color = Color.white;
    }
}
