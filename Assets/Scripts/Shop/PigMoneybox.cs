using System;
using System.Collections;
using System.Globalization;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class PigMoneybox : MonoBehaviour
    {
        [SerializeField]
        private GameObject _signal;


        private int _points;

        public static int MaxPoints { get; set; } = 5000;

        public static DateTime NextClaim
        {
            get => DateTime.Parse(PlayerPrefs.GetString("NextClaim",
                DateTime.MinValue.ToString(CultureInfo.CurrentCulture)),CultureInfo.CurrentCulture);
            set => PlayerPrefs.SetString("NextClaim", value.ToString(CultureInfo.CurrentCulture));
        }

        [Header("Panel moneybox"), SerializeField]
        private GameObject _moneyboxPanel;

        [SerializeField]
        private Text _nextClaimText;

        [SerializeField]
        private Text _buffText;

        [SerializeField]
        private Text _maxText;

        [SerializeField]
        private Text _getText;

        [SerializeField]
        private Text _getX2Text;

        private void Start()
        {
            YG.YandexGame.GetDataEvent += () =>
            {
                Debug.Log("Next claim: " + NextClaim);
                if (NextClaim != DateTime.MinValue)
                {
                    if (NextClaim < DateTime.Now)
                    {
                        _points = MaxPoints;
                        openMoneybox();
                    }
                    else
                    {
                        countPoints();
                    }
                }
                else
                {
                    NextClaim = DateTime.Now.AddMinutes((float) MaxPoints /
                                                        (PlayerDataController.LevelSum * 15));
                }
            };
        }

       


        private IEnumerator checkSignal()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                if (MaxPoints == _points && !_signal.activeSelf)
                {
                    _signal.SetActive(true);
                }
                else if (MaxPoints != _points && _signal.activeSelf)
                {
                    _signal.SetActive(false);
                }
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void OpenMoneybox()
        {
            countPoints();
            openMoneybox();
        }

        private void openMoneybox()
        {
            Debug.Log("Total minutes: " + (NextClaim - DateTime.Now).TotalMinutes);
            _moneyboxPanel.SetActive(true);

            _nextClaim = TimeSpan.FromMinutes((float) (MaxPoints - _points) /
                                              (PlayerDataController.LevelSum * 15));
            _nextClaimText.text = _nextClaim.ToString("T");
            if (_time == null)
                _time = StartCoroutine(time());
            else
            {
                StopCoroutine(_time);
                _time = StartCoroutine(time());
            }

            _getText.text = _points.ToString();
            _getX2Text.text = (3 * _points).ToString();
            _buffText.text = $"You get: {PlayerDataController.LevelSum * 15} coin every min";
            _maxText.text = $"Maximum: {GameManager.NormalSum(MaxPoints)} coins";
        }

        private TimeSpan _nextClaim;
        private Coroutine _time;

        private IEnumerator time()
        {
            while (true)
            {
                if (_nextClaim.TotalSeconds > 0)
                {
                    _nextClaim = _nextClaim.Add(new TimeSpan(0, 0, -1));
                    _nextClaimText.text = _nextClaim.ToString(@"hh\:mm\:ss");
                }

                yield return new WaitForSeconds(1f);
            }
        }

        private void countPoints()
        {
            _points = (int) (MaxPoints -
                             Math.Ceiling((NextClaim - DateTime.Now).TotalMinutes * PlayerDataController.LevelSum *
                                          15));
        }

        public void Claim()
        {
            _moneyboxPanel.SetActive(false);

            NextClaim = DateTime.Now.AddMinutes((float) MaxPoints /
                                                (PlayerDataController.LevelSum * 15));
            PlayerDataController.PointSum += _points;
            _points = 0;
        }

        public void ClaimX3()
        {
            _moneyboxPanel.SetActive(false);
            NextClaim = DateTime.Now.AddMinutes((float) (MaxPoints) /
                                                (PlayerDataController.LevelSum * 15));
            PlayerDataController.PointSum += 3 * _points;
            _points = 0;
        }
    }
}