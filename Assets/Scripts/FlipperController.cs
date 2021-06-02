using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipperController : MonoBehaviour
{
    public bool IsFlipper { get; set; }
    HingeJoint2D hj;
    
 
   
    float y;

    private void Start()
    {

        hj = GetComponent<HingeJoint2D>();
      
      
       
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
        if(GameManager.automod && other.tag == "Player")
        IsFlipper = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.automod && collision.tag == "Player")
            IsFlipper = false;
    }


    
}
