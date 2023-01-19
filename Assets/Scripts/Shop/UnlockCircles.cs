using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class UnlockCircles : MonoBehaviour
    {
        public static UpgradeCircle upgrade;


        private static UnlockCircles instance;

        [SerializeField]
        private Image[] _levelForCircle;

        [SerializeField]
        private GameObject _levelCurrent;

        [SerializeField]
        private GameObject _levelElements;

        [SerializeField]
        private Text _countOfElements;


        private static readonly int[] maxUpgrade = {12, 14, 12, 15, 16, 14, 16, 16, 14};

        public static readonly bool[] IsMax = {false, false, false, false, false, false, false, false, false};

        private const int START_COST = 3;
        private const int MULTI_COST = 3;

        private static readonly int[] costHit = {10, 10, 10, 10, 10, 10, 10, 10, 10};

        private void Awake()
        {
            instance = this;
            MenuController.openMenu[MenuController.Shops.UpgradeFields] += UpdateQuestText;
        }

        private void Start()
        {
            if (maxUpgrade[0] != GameManager.instance.fields[0].circles.Length - 1)
            {
                Debug.LogError(
                    $"Max unlock circle ({maxUpgrade}) not equal current circle on field ({GameManager.instance.fields[0].circles.Length - 1})");
            }

            FieldManager.openOneField += UpdateLevelFill;
            
            for (int _field = 0; _field < FieldManager.fields.isOpen.Length; _field++)
            {
                if (!FieldManager.fields.isOpen[_field]) continue;
                for (int _i = 0; _i < upgrade.upgrades[_field]; _i++)
                {
                    OpenCircle(_field, _i);
                }
                if (upgrade.upgrades[_field] >= maxUpgrade[_field])
                {
                    IsMax[_field] = true;
                    continue;
                }
                costHit[_field] = START_COST + MULTI_COST * upgrade.upgrades[_field];
               
            }
            UpdateLevelFill();
        }

        private void UpdateLevelFill()
        {
            if (IsMax[FieldManager.currentField])
            {
                _levelCurrent.SetActive(true);
                _levelElements.SetActive(false);
            }
            else
            {
                _levelCurrent.SetActive(false);
                _levelElements.SetActive(true);
                for (int _i = 0; _i < _levelForCircle.Length; _i++)
                {
                    if (_i >= maxUpgrade[FieldManager.currentField] && _levelForCircle[_i].gameObject.activeSelf)
                    {
                        _levelForCircle[_i].gameObject.SetActive(false);
                    }
                    else if (_i < maxUpgrade[FieldManager.currentField] &&
                             !_levelForCircle[_i].gameObject.activeSelf)
                    {
                        _levelForCircle[_i].gameObject.SetActive(true);
                    }

                    _levelForCircle[_i].fillAmount = upgrade.upgrades[FieldManager.currentField] <= _i ? 0 : 1;
                }

                _countOfElements.text = (GameManager.instance.fields[FieldManager.currentField].circles.Length -
                                         maxUpgrade[FieldManager.currentField] +
                                         upgrade.upgrades[FieldManager.currentField]).ToString();
                _levelForCircle[upgrade.upgrades[FieldManager.currentField]].fillAmount =
                    (float) upgrade.experience[FieldManager.currentField] / costHit[FieldManager.currentField];
            }
        }

        private void UpdateQuestText()
        {
        }

        public static void AddExp(int field, int exp)
        {
            upgrade.experience[field] += exp;
            if (FieldManager.currentField == field)
            {
                instance.UpdateFill();
            }
            if (upgrade.experience[field] < costHit[field]) return;
            upgrade.experience[field] = 0;
            BuyCircle(field);
            if (upgrade.upgrades[field] >= maxUpgrade[field])
                IsMax[field] = true;

            if (!IsMax[field]||FieldManager.currentField != field) return;
            instance._levelCurrent.SetActive(true);
            AnalyticManager.GetFirstLevel(field);
            instance._levelElements.SetActive(false);
        }

        private void UpdateFill()
        {
            _levelForCircle[upgrade.upgrades[FieldManager.currentField]].fillAmount =
                (float) upgrade.experience[FieldManager.currentField] / costHit[FieldManager.currentField];
        }

        private static void OpenCircle(int fieldNumber, int circle)
        {
            if (circle < GameManager.instance.fields[fieldNumber].circles.Length - 1)
                GameManager.instance.fields[fieldNumber].circles[circle].SetActive(true);
            if (circle == maxUpgrade[fieldNumber] - 1)
            {
                IsMax[fieldNumber] = true;
                
            }
        }

        private static void BuyCircle(int fieldNumber)
        {
            OpenCircle(fieldNumber, upgrade.upgrades[fieldNumber]);
            upgrade.upgrades[fieldNumber]++;
            costHit[fieldNumber] = START_COST + MULTI_COST * upgrade.upgrades[fieldNumber];
        }
    }

    public class UpgradeCircle
    {
        public int[] upgrades;

        public int[] experience;

        public UpgradeCircle()
        {
            experience = new int[9];
            upgrades = new int[9];
            upgrades[0] = 6;
        }
    }
}