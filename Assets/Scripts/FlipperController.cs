using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlipperController : MonoBehaviour
{
    public static bool[] IsFlipper { get; set; } = new bool[] { false, false };
    HingeJoint2D hjright;
    public HingeJoint2D hjleft;
    BoxCollider2D cc2dright;
    public static bool rightorleft;
    int flag = 1;
    public AudioSource As;
    public AudioClip Ac;
    public int Field = 0;
    public static bool AllFieldTogether { get; set; } = false;
    private void Start()
    {

        hjright = GetComponent<HingeJoint2D>();
        cc2dright = GetComponent<BoxCollider2D>();
      
       
    }

    public void Audio()
    {

        As.PlayOneShot(Ac);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Field == FieldManager.CorrectField || GameManager.automod[Field] || AllFieldTogether)
            if (IsFlipper[Field] || AllFieldTogether)
            {


                if (rightorleft)
                    hjright.useMotor = true;
                else
                    hjleft.useMotor = true;

             }
        else
        {
                hjright.useMotor = false;

                hjleft.useMotor = false;
             
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.automod[Field] && (other.tag == "Player"|| other.tag == "PlayerF2"))
        {
           
            Audio();
            rightorleft = true;
            IsFlipper[Field] = true;
            cc2dright.size = new Vector2(cc2dright.size.x - flag * 0.4f, cc2dright.size.y);
            cc2dright.offset = new Vector2(cc2dright.offset.x - flag * 0.4f , cc2dright.offset.y);
            flag = -flag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.automod[Field] && (collision.tag == "Player"|| collision.tag == "PlayerF2"))
        {
            IsFlipper[Field] = false;
        }
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
