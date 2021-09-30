using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperLeft : MonoBehaviour
{
    public int Field = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.automod[Field] && (other.tag == "Player" || other.tag == "PlayerF2"))
        {
           
            As.PlayOneShot(Ac);
            FlipperController.rightorleft = false;
           FlipperController.IsFlipper[Field] = true;
            cc2dright.size = new Vector2(cc2dright.size.x - flag * 0.4f , cc2dright.size.y);
            cc2dright.offset = new Vector2(cc2dright.offset.x - flag * 0.4f, cc2dright.offset.y);
            flag = -flag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.automod[Field] && (collision.tag == "Player" || collision.tag == "PlayerF2"))
        {
            FlipperController.IsFlipper[Field] = false;
        }
    }

    BoxCollider2D cc2dright;
    public static bool rightorleft;
    int flag = 1;
    public AudioSource As;
    public AudioClip Ac;


    private void Start()
    {

        cc2dright = GetComponent<BoxCollider2D>();


    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerF2")
        {
            if (GameManager.choosenBall == 1 && collision.gameObject.GetComponent<Mainball>())
            {
                collision.collider.isTrigger = true;
            }
        }
    }
}
