using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlipperController : MonoBehaviour
{
    public static bool[] IsFlipper { get; set; } = new bool[] { false, false };
    private FlipperLeft flipperLeft;
    HingeJoint2D hjright;
    public HingeJoint2D hjleft;
    BoxCollider2D cc2dright;
    public static bool rightorleft;
    int flag = 1;
    public AudioSource As;
    public static bool AllFieldTogether { get; set; } = false;
    private void Start()
    {
        flipperLeft = hjleft.gameObject.GetComponent<FlipperLeft>();
        hjright = GetComponent<HingeJoint2D>();
        cc2dright = GetComponent<BoxCollider2D>();


    }

    public void Audio()
    {
        if (As != null)
            As.Play();
    }

    // Update is called once per frame
    void Update()
    {


        if (IsFlipper[flipperLeft.Field] || AllFieldTogether)
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
        if (StandartBuff.automod[flipperLeft.Field] && other.tag == "Player")
        {

            Audio();
            rightorleft = true;
            IsFlipper[flipperLeft.Field] = true;
            cc2dright.size = new Vector2(cc2dright.size.x - flag * 0.4f, cc2dright.size.y);
            cc2dright.offset = new Vector2(cc2dright.offset.x - flag * 0.4f, cc2dright.offset.y);
            flag = -flag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (StandartBuff.automod[flipperLeft.Field] && collision.tag == "Player")
        {
            IsFlipper[flipperLeft.Field] = false;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.choosenBall == 1 && collision.gameObject.GetComponent<Mainball>())
            {
                collision.gameObject.layer = 9;
            }
        }
    }
}
