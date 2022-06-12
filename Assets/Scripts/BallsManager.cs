using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public static StatsBall balls;

    private static int costFieldBall = 500;
    [SerializeField]
    private GameObject[] _fieldBalls;

    [SerializeField]
    private GameObject _buyFieldBallButton;
    private void Start()
    {
        if (balls.fieldBallCount > 0)
        {
            StartCoroutine(firstSpawnFieldBall());
            if (balls.fieldBallCount == 9)
            {
                _buyFieldBallButton.SetActive(false);
            }
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
    
    
    public void BuyFieldBall()
    {
        
        if (PlayerDataController.Gems < costFieldBall)
        {
            GameManager.instance.shop.SetActive(true);
            AnalyticManager.OpenDonateShop();
            return;
        }
        _fieldBalls[balls.fieldBallCount].SetActive(true);
        balls.fieldBallCount++;
        PlayerDataController.Gems -= costFieldBall;
        if (balls.fieldBallCount == 9)
        {
            _buyFieldBallButton.SetActive(false);
        }
    }
    
}


public class StatsBall
{
    public int fieldBallCount;
}