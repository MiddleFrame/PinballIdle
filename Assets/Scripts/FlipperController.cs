using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    public bool isFlipper { get; set; }
    HingeJoint2D hj;
    private void Start()
    {

        hj = GetComponent<HingeJoint2D>();
      
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlipper)
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
}
