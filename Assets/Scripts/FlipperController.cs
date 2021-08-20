using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipperController : MonoBehaviour
{
    public bool IsFlipper { get; set; }
    HingeJoint2D hj;
    CircleCollider2D cc2d;
    int flag = 1;
   
   

    private void Start()
    {

        hj = GetComponent<HingeJoint2D>();
        cc2d = GetComponent<CircleCollider2D>();
      
       
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
            cc2d.offset += new Vector2(flag*0.15f, 0) ;
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
