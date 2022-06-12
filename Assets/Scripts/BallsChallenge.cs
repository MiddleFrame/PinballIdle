using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsChallenge : MonoBehaviour
{
    public int timeOnField;

    private void Start()
    {
        InvokeRepeating("StartTime",0f, 1f);
    }

    private void StartTime()
    {
       
            timeOnField++;
        
    }
}
