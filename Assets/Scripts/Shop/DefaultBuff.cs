using System;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class DefaultBuff : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        private Text _costText;

        [SerializeField]
        private Text _costBonusText;

        [SerializeField]
        private Text _costExpBonusText;

        [SerializeField]
        private Text _costAutoFlipperText;

        [SerializeField]
        private Text _pointBuffText;

        [SerializeField]
        private Text _pointBonusText;

        [SerializeField]
        private Text _pointExpBonusText;

        private bool _isOpen = true;
        private bool _isOpenAutoFlipper = true;
        private bool _isBonusOpen = true;
        private bool _isExpBonusOpen = true;
        public static CostAndGrade grade;
        private readonly int[] _costOnGrade = {100, 100, 100, 100, 100, 100, 100, 100, 100};
        private readonly int[] _costOnBonusGrade = {500, 500, 500, 500, 500, 500, 500, 500, 500};
        private readonly int[] _costOnExpBonusGrade = {500, 500, 500, 500, 500, 500, 500, 500, 500};
        private const int COST_AUTO_FLIPPER = 10000;

        [SerializeField]
        private Text _autoFlipperText;

        [SerializeField]
        private Image _buttonImage;

        [SerializeField]
        private Image _buttonBonusImage;

        [SerializeField]
        private Image _buttonExpBonusImage;

        [SerializeField]
        private Image _buttonAutoFlipperImage;

        [SerializeField]
        private GameObject _autoModSlider;

        public static bool[] autoMod;

        private void Awake()
        {
            MenuController.shopOpen[0] += ChangeHitText;
            MenuController.shopOpen[0] += ChangeBonusText;
            MenuController.shopOpen[0] += ChangeExpBonusText;
            MenuController.shopOpen[1] += OpenAutoMod;
        }

        private void Start()
        {
            autoMod = new bool[grade.autoFlippers.Length];
            for (int _i = 0; _i < FieldManager.fields.isOpen.Length; _i++)
            {
                if (!FieldManager.fields.isOpen[_i]) continue;
                BuffHit(_i, grade.pointOnBit[_i] - 1);
                BuffBonusTime(_i, (grade.bonusTime[_i] - 30) / 5);
                BuffExpBonusTime(_i, (grade.expTime[_i] - 30) / 5);
                if (grade.autoFlippers[_i])
                {
                    AutoMod(_i);
                }
            }
            if(grade.autoFlippers[0])
                ChallengeManager.Instance.OpenChallenges();
        }

        private void Update()
        {
            if (FieldManager.currentField == -1)
                return;

            switch (MenuController.currentMenu)
            {
                case 0:
                {
                    switch (_isOpen)
                    {
                        case true when PlayerDataController.PointSum < _costOnGrade[FieldManager.currentField]:
                            _isOpen = false;
                            _buttonImage.raycastTarget = false;
                            _buttonImage.sprite = GameManager.instance._lockedSprite;
                            GameManager.TextDown(_costText.gameObject);
                            break;
                        case false when PlayerDataController.PointSum >= _costOnGrade[FieldManager.currentField]:
                            _isOpen = true;
                            _buttonImage.sprite = GameManager.instance._unlockedSprite;
                            _buttonImage.raycastTarget = true;
                            GameManager.TextUp(_costText.gameObject);
                            break;
                    }

                    switch (_isBonusOpen)
                    {
                        case true when PlayerDataController.PointSum < _costOnBonusGrade[FieldManager.currentField]:
                            _isBonusOpen = false;
                            _buttonBonusImage.raycastTarget = false;
                            _buttonBonusImage.sprite = GameManager.instance._lockedSprite;
                            GameManager.TextDown(_costBonusText.gameObject);
                            break;
                        case false when PlayerDataController.PointSum >= _costOnBonusGrade[FieldManager.currentField]:
                            _isBonusOpen = true;
                            _buttonBonusImage.sprite = GameManager.instance._unlockedSprite;
                            _buttonBonusImage.raycastTarget = true;
                            GameManager.TextUp(_costBonusText.gameObject);
                            break;
                    }

                    switch (_isExpBonusOpen)
                    {
                        case true when PlayerDataController.PointSum < _costOnExpBonusGrade[FieldManager.currentField]:
                            _isExpBonusOpen = false;
                            _buttonExpBonusImage.raycastTarget = false;
                            _buttonExpBonusImage.sprite = GameManager.instance._lockedSprite;
                            GameManager.TextDown(_costExpBonusText.gameObject);
                            break;
                        case false
                            when PlayerDataController.PointSum >= _costOnExpBonusGrade[FieldManager.currentField]:
                            _isExpBonusOpen = true;
                            _buttonExpBonusImage.sprite = GameManager.instance._unlockedSprite;
                            _buttonExpBonusImage.raycastTarget = true;
                            GameManager.TextUp(_costExpBonusText.gameObject);
                            break;
                    }

                    break;
                }
                case 1 when _isOpenAutoFlipper && !grade.autoFlippers[FieldManager.currentField] &&
                            PlayerDataController.PointSum < COST_AUTO_FLIPPER:
                    _isOpenAutoFlipper = false;

                    _buttonAutoFlipperImage.raycastTarget = false;
                    _buttonAutoFlipperImage.sprite = GameManager.instance._lockedSprite;
                    GameManager.TextDown(_costAutoFlipperText.gameObject);
                    break;
                case 1:
                {
                    if (!_isOpenAutoFlipper && !grade.autoFlippers[FieldManager.currentField] &&
                        PlayerDataController.PointSum >= COST_AUTO_FLIPPER)
                    {
                        _isOpenAutoFlipper = true;

                        _buttonAutoFlipperImage.sprite = GameManager.instance._unlockedSprite;
                        _buttonAutoFlipperImage.raycastTarget = true;
                        GameManager.TextUp(_costAutoFlipperText.gameObject);
                    }

                    break;
                }
            }
        }

        private void BuffHit(int field = 0, int countGrades = 1)
        {
            if (!FieldManager.fields.isOpen[field])
                return;
            for (int _i = 0; _i < countGrades; _i++)
            {
                _costOnGrade[field] = (int) (_costOnGrade[field] * 1.1f);
            }
        }

        private void ChangeHitText()
        {
            _costText.text = GameManager.NormalSum(_costOnGrade[FieldManager.currentField]);
            _pointBuffText.text = grade.pointOnBit[FieldManager.currentField] + "→" +
                                  (grade.pointOnBit[FieldManager.currentField] + 1);
        }

        private void ChangeBonusText()
        {
            _costBonusText.text = GameManager.NormalSum(_costOnBonusGrade[FieldManager.currentField]);
            _pointBonusText.text = grade.bonusTime[FieldManager.currentField] + "→" +
                                   (grade.bonusTime[FieldManager.currentField] + 5);
        }

        private void ChangeExpBonusText()
        {
            _costExpBonusText.text = GameManager.NormalSum(_costOnExpBonusGrade[FieldManager.currentField]);
            _pointExpBonusText.text = grade.expTime[FieldManager.currentField] + "→" +
                                      (grade.expTime[FieldManager.currentField] + 5);
        }


        private void OpenAutoMod()
        {
            if (grade.autoFlippers[FieldManager.currentField])
            {
                _autoFlipperText.text = "Auto-flippers";
                _autoModSlider.SetActive(true);
                _buttonAutoFlipperImage.gameObject.SetActive(false);
            }
            else
            {
                _autoFlipperText.text = "Buy Auto-flippers";
                _autoModSlider.SetActive(false);
                _buttonAutoFlipperImage.gameObject.SetActive(true);
            }
        }

        public void BuyBuffHit()
        {
            if (PlayerDataController.PointSum < _costOnGrade[FieldManager.currentField]) return;
            PlayerDataController.PointSum -= _costOnGrade[FieldManager.currentField];
            //statistic
            Statistics.stats.pointSpent += _costOnGrade[FieldManager.currentField];

            grade.pointOnBit[FieldManager.currentField] += 1;
            BuffHit(FieldManager.currentField);
            ChangeHitText();
        }


        private void BuffBonusTime(int field = 0, int countGrades = 1)
        {
            if (!FieldManager.fields.isOpen[field])
                return;
            for (int _i = 0; _i < countGrades; _i++)
            {
                _costOnBonusGrade[field] = (int) (_costOnBonusGrade[field] * 1.1f);
            }
        }


        public void BuyBuffBonusTime()
        {
            if (PlayerDataController.PointSum < _costOnBonusGrade[FieldManager.currentField]) return;
            PlayerDataController.PointSum -= _costOnBonusGrade[FieldManager.currentField];
            //statistic
            Statistics.stats.pointSpent += _costOnBonusGrade[FieldManager.currentField];

            grade.bonusTime[FieldManager.currentField] += 5;
            BuffBonusTime(FieldManager.currentField);
            ChangeBonusText();
        }

        private void BuffExpBonusTime(int field = 0, int countGrades = 1)
        {
            if (!FieldManager.fields.isOpen[field])
                return;
            for (int _i = 0; _i < countGrades; _i++)
            {
                _costOnExpBonusGrade[field] = (int) (_costOnExpBonusGrade[field] * 1.1f);
            }
        }


        public void BuyBuffExpBonusTime()
        {
            if (PlayerDataController.PointSum < _costOnExpBonusGrade[FieldManager.currentField]) return;
            PlayerDataController.PointSum -= _costOnExpBonusGrade[FieldManager.currentField];
            //statistic
            Statistics.stats.pointSpent += _costOnExpBonusGrade[FieldManager.currentField];

            grade.expTime[FieldManager.currentField] += 5;
            BuffExpBonusTime(FieldManager.currentField);
            ChangeExpBonusText();
        }

        public void BuyAutoMod()
        {
            if (PlayerDataController.PointSum < COST_AUTO_FLIPPER) return;
            PlayerDataController.PointSum -= COST_AUTO_FLIPPER;
            Statistics.stats.pointSpent += COST_AUTO_FLIPPER;
            grade.autoFlippers[FieldManager.currentField] = true;
            OpenAutoMod();
            AutoMod();
            ChallengeManager.Instance.OpenChallenges();
        }
        
        public void AutoMod(int field = -1)
        {
            if (field == -1)
            {
                autoMod[FieldManager.currentField] = !autoMod[FieldManager.currentField];
                if (autoMod[FieldManager.currentField])
                {
                    var _localScale = _autoModSlider.transform.localScale;
                    _localScale = new Vector3(1, _localScale.y, _localScale.z);
                    _autoModSlider.transform.localScale = _localScale;
                    _autoModSlider.GetComponent<Image>().color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
                }
                else
                {
                    var _localScale = _autoModSlider.transform.localScale;
                    _localScale = new Vector3(-1, _localScale.y, _localScale.z);
                    _autoModSlider.transform.localScale = _localScale;
                    _autoModSlider.GetComponent<Image>().color = Color.red;
                }
            }
            else
                autoMod[field] = !autoMod[field];

            
        }
    }

    [Serializable]
    public class CostAndGrade
    {
        public int[] pointOnBit;
        public bool[] autoFlippers;
        public int[] bonusTime;
        public int[] expTime;

        public CostAndGrade()
        {
            pointOnBit = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
            bonusTime = new[] {30, 30, 30, 30, 30, 30, 30, 30, 30};
            expTime = new[] {30, 30, 30, 30, 30, 30, 30, 30, 30};
            autoFlippers = new[] {false, false, false, false, false, false, false, false, false};
        }
    }
}