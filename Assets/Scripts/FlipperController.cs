using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipperController : MonoBehaviour
{
    public bool IsFlipper { get; set; }
    HingeJoint2D hj;
    BoxCollider2D cc2d;
    int flag = 1;
    public AudioSource As;
    public AudioClip Ac;


    private void Start()
    {

        hj = GetComponent<HingeJoint2D>();
        cc2d = GetComponent<BoxCollider2D>();
      
       
    }

    public void Audio()
    {
        As.PlayOneShot(Ac);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (IsFlipper)
        {
            hj.useMotor = true;
           
            /* var motor = hj.motor;
              motor.motorSpeed = 2000f;
              hj.motor = motor;*/
        }
        else
        {
            hj.useMotor = false;

            
              /* var motor = hj.motor;
               motor.motorSpeed = -2000f;
               hj.motor = motor;*/
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.automod && other.tag == "Player")
        {
            
            IsFlipper = true;
            cc2d.size = new Vector2(cc2d.size.x - flag * 0.4f, cc2d.size.y) ;
            cc2d.offset = new Vector2(cc2d.offset.x - flag * 0.4f, cc2d.offset.y) ;
            flag = -flag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.automod && collision.tag == "Player")
        {
            IsFlipper = false;
        }
    }


    
}
