using System;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class BuyStopper : MonoBehaviour
    {
        public static StopperGrades grades;

        private const int COST_STOPPERS = 40000;

        [SerializeField]
        private Image _buyStoppers;

        [SerializeField]
        private GameObject[] _stoppers;

        [SerializeField]
        private Text _costStoppersText;

        [SerializeField]
        private Text _buyStoppersText;

        private bool _isStopperOpen = true;

        private void Awake()
        {
            MenuController.shopOpen[1] += changeText;
        }

        private void Start()
        {
            YG.YandexGame.GetDataEvent += () =>
            {
                for (int _i = 0; _i < FieldManager.fields.isOpen.Length; _i++)
                {
                    if (!FieldManager.fields.isOpen[_i]) continue;
                    if (grades.isStopper[_i])
                        openStoppers(_i);
                }
            };
        }

        private void Update()
        {
            if (FieldManager.currentField == -1)
            {
                return;
            }

            if (MenuController.currentMenu == 1 && !grades.isStopper[FieldManager.currentField])
            {
                if (_isStopperOpen && PlayerDataController.PointSum < COST_STOPPERS)
                {
                    _isStopperOpen = false;
                    _buyStoppers.raycastTarget = false;
                    _buyStoppers.sprite = GameManager.instance._lockedSprite;
                    GameManager.TextDown(_costStoppersText.gameObject);
                }
                else if (!_isStopperOpen && PlayerDataController.PointSum >= COST_STOPPERS)
                {
                    _isStopperOpen = true;
                    _buyStoppers.sprite = GameManager.instance._unlockedSprite;
                    _buyStoppers.raycastTarget = true;
                    GameManager.TextUp(_costStoppersText.gameObject);
                }
            }
        }

        public void BuyStoppers()
        {
            if (PlayerDataController.PointSum < COST_STOPPERS) return;
            PlayerDataController.PointSum -= COST_STOPPERS;
            Statistics.stats.pointSpent += COST_STOPPERS;
            grades.isStopper[FieldManager.currentField] = true;
            openStoppers(FieldManager.currentField);
            changeText();
        }

        private void openStoppers(int i)
        {
            _stoppers[i].SetActive(true);
        }

        private void changeText()
        {
            if (grades.isStopper[FieldManager.currentField])
            {
                _buyStoppersText.text = "Stoppers";
                _buyStoppers.gameObject.SetActive(false);
            }
            else
            {
                _buyStoppersText.text = "Buy stoppers";
                _buyStoppers.gameObject.SetActive(true);
            }
        }
    }

    [Serializable]
    public class StopperGrades
    {
        public bool[] isStopper;

        public StopperGrades(int length)
        {
            isStopper = new bool[length];
        }
    }
}