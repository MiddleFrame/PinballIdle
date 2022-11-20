using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public static StatsBall balls;

    private static int costFieldBall = 200;
    [SerializeField]
    private GameObject[] _fieldBalls;

    [SerializeField]
    private GameObject _buyFieldBallButton;
    private void Start()
    {
        if (balls.fieldBallCount > 0)
        {
            if (balls.fieldBallCount > 9)
            {
                balls.fieldBallCount = 9;
            }
            StartCoroutine(firstSpawnFieldBall());
            
        }
        
    }

    private IEnumerator firstSpawnFieldBall()
    {
        for (int _i = 0; _i < balls.fieldBallCount; _i++)
        {
            yield return new WaitForSeconds(1f);
            _fieldBalls[_i].SetActive(true);
        }
    }
    
    
    public void OpenFieldBall()
    {
        if (balls.fieldBallCount == 9)
            return;
        
        _fieldBalls[balls.fieldBallCount].SetActive(true);
        balls.fieldBallCount++;
    }
    
}


public class StatsBall
{
    public int fieldBallCount;
}