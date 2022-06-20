using System;
using System.Collections;
using System.Collections.Generic;
using Competition;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopCompetitive : MonoBehaviour
{
    [SerializeField]
    private Image _startCompetitive;

    private bool _isOpen = true;

    [SerializeField]
    private Sprite _openSprite;

    [SerializeField]
    private Sprite _closeSprite;

    [SerializeField]
    private GameObject _costText;
    private void Update()
    {
        if (_isOpen && PlayerDataController.PointSum < 1000000)
        {
            _isOpen = false;
            _startCompetitive.raycastTarget = false;
            _startCompetitive.sprite = _closeSprite;
            GameManager.TextDown(_costText);
        }
        else if (!_isOpen && PlayerDataController.PointSum >= 1000000)
        {
            _isOpen = true;
            _startCompetitive.raycastTarget = true;
            _startCompetitive.sprite = _openSprite;
            
            GameManager.TextUp(_costText);
        }
    }

    public void BuyCompetitive()
    {
        PlayerDataController.PointSum -= 1000000;
    }


    public void StartCompetitive()
    {
        CompetitionManager.isBuff[0] = false;
        SceneManager.LoadScene(1);
    }

    public void OnRecieveReward()
    {
        CompetitionManager.isBuff[0] = true;
        SceneManager.LoadScene(1);
    }
    
}