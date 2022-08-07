using System;
using System.Collections;
using Competition;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shop
{
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

        [SerializeField]
        private GameObject _findWindow;

        [SerializeField]
        private GameObject _ruleWindow;

        [SerializeField]
        private Text _players;

        [SerializeField]
        private Text _tips;

        [SerializeField]
        private Text _timer;

        private static readonly string[] tips =
        {
            "Before the enemy trap takes away your points, you have time to quickly buy an upgrade.",
            "If a player doubled his points at the beginning of the game, it will be very difficult to defeat him without improvement.",
            "The area at the top of the field gives three times as many points.",
            "Keep an eye on your enemy's score, maybe you should \"burn\" them.",
            "The trap, unlike the other improvements, does not get more expensive.",
            "If you buy a new kind of ball or field, other players will also see it",
            "The second improvement doubles the points you receive, but only lasts 30 seconds."
        };

        private void Update()
        {
            YG.YandexGame.GetDataEvent += () =>
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
            };
        }

        public void BuyCompetitive()
        {
            PlayerDataController.PointSum -= 1000000;
        }


        public void StartCompetitive()
        {
            _ruleWindow.SetActive(false);
            _findWindow.SetActive(true);
            CompetitionManager.isBuff[0] = false;
            StartCoroutine(FindCompetitive());
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            var _time = new DateTime();
            while (true)
            {
                _time = _time.AddSeconds(1);
                _timer.text = _time.ToString("mm:ss");
                if (_time.Second%7 == 0)
                {
                    _tips.text = tips[Random.Range(0, tips.Length)];
                }
                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator FindCompetitive()
        {
            _tips.text = tips[Random.Range(0, tips.Length)];
            for (int _i = 0; _i < 9;)
            {
                yield return new WaitForSeconds(0.1f);
                if (Random.Range(0, 1.0f) < 0.9f) continue;
                _i++;
                _players.text = $"{_i}/9";
            }

            SceneManager.LoadScene(1);
        }

        public void OnReceiveReward()
        {
            _ruleWindow.SetActive(false);
          
                _findWindow.SetActive(true);
                CompetitionManager.isBuff[0] = true;
                StartCoroutine(FindCompetitive());
                StartCoroutine(Timer());
          
            
        }
    }
}