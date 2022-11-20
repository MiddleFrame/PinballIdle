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
        //
        // [SerializeField]
        // private Text _costBonusText;
        //
        // [SerializeField]
        // private Text _costExpBonusText;
        //
        // [SerializeField]
        // private Text _costAutoFlipperText;

        [SerializeField]
        private Text _pointBuffText;

        // [SerializeField]
        // private Text _pointBonusText;
        //
        // [SerializeField]
        // private Text _pointExpBonusText;

        private bool _isOpen = true;

        private static DefaultBuff _instance;
        //private bool _isOpenAutoFlipper = true;
        //private bool _isBonusOpen = true;
        // private bool _isExpBonusOpen = true;
        public static CostAndGrade grade;

        public static readonly int[] costOnGrade =
        {
            DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST,
            DEFAULT_COST, DEFAULT_COST
        };

        public const int DEFAULT_COST = 100;
        // private readonly int[] _costOnBonusGrade = {500, 500, 500, 500, 500, 500, 500, 500, 500};
        // private readonly int[] _costOnExpBonusGrade = {500, 500, 500, 500, 500, 500, 500, 500, 500};
        // private const int COST_AUTO_FLIPPER = 10000;

        // [SerializeField]
        // private Text _autoFlipperText;

        [SerializeField]
        private Image _buttonImage;

        // [SerializeField]
        // private Image _buttonBonusImage;

        // [SerializeField]
        // private Image _buttonExpBonusImage;

        //[SerializeField]
        // private Image _buttonAutoFlipperImage;


        public static bool[] autoMod;

        private void Awake()
        {
            _instance = this;
            MenuController.openMenu[MenuController.Shops.UpgradeFields] += ChangeHitText;
        }

        private void Start()
        {
            autoMod = new bool[grade.autoFlippers.Length];
            for (int _i = 0; _i < FieldManager.fields.isOpen.Length; _i++)
            {
                if (!FieldManager.fields.isOpen[_i]) continue;
                BuffHit(_i, grade.pointOnBit[_i] - 1);
                // BuffBonusTime(_i, (grade.bonusTime[_i] - 30) / 5);
                // BuffExpBonusTime(_i, (grade.expTime[_i] - 30) / 5);
                if (grade.autoFlippers[_i])
                {
                    autoMod[_i] = true;
                }
            }

            if (grade.autoFlippers[0])
                ChallengeManager.Instance.OpenChallenges();
        }


        public static void ResetProgress(int field)
        {
            grade.multiplyPoint[field] += 0.2f;
            grade.autoFlippers[field] = false;
            autoMod[field] = false;
            grade.pointOnBit[field] = 1;
            costOnGrade[field] = DEFAULT_COST;
            _instance.ChangeHitText();
        }
        
        private void Update()
        {
            if (FieldManager.currentField == -1)
                return;

            if (MenuController.currentMenu != (int) MenuController.Shops.UpgradeFields) return;
            switch (_isOpen)
            {
                case true when PlayerDataController.PointSum < costOnGrade[FieldManager.currentField]:
                    _isOpen = false;
                    _buttonImage.raycastTarget = false;
                    _buttonImage.sprite = GameManager.instance._lockedSprite;
                    GameManager.TextDown(_costText.transform.parent.gameObject);
                    break;
                case false when PlayerDataController.PointSum >= costOnGrade[FieldManager.currentField]:
                    _isOpen = true;
                    _buttonImage.sprite = GameManager.instance._unlockedSprite;
                    _buttonImage.raycastTarget = true;
                    GameManager.TextUp(_costText.transform.parent.gameObject);
                    break;
            }
        }

        private void BuffHit(int field = 0, int countGrades = 1)
        {
            if (!FieldManager.fields.isOpen[field])
                return;
            for (int _i = 0; _i < countGrades; _i++)
            {
                costOnGrade[field] = (int) (costOnGrade[field] * 1.1f);
            }
        }

        private void ChangeHitText()
        {
            _costText.text = GameManager.NormalSum(costOnGrade[FieldManager.currentField]);
            _pointBuffText.text = "Field Element gain " + grade.pointOnBit[FieldManager.currentField];
        }

        // private void ChangeBonusText()
        // {
        //     _costBonusText.text = GameManager.NormalSum(_costOnBonusGrade[FieldManager.currentField]);
        //     _pointBonusText.text = grade.bonusTime[FieldManager.currentField] + "→" +
        //                            (grade.bonusTime[FieldManager.currentField] + 5);
        // }

        // private void ChangeExpBonusText()
        // {
        //     _costExpBonusText.text = GameManager.NormalSum(_costOnExpBonusGrade[FieldManager.currentField]);
        //     _pointExpBonusText.text = grade.expTime[FieldManager.currentField] + "→" +
        //                               (grade.expTime[FieldManager.currentField] + 5);
        // }


        // private void OpenAutoMod()
        // {
        //     if (grade.autoFlippers[FieldManager.currentField])
        //     {
        //         _autoFlipperText.text = "Auto-flippers";
        //         _buttonAutoFlipperImage.gameObject.SetActive(false);
        //     }
        //     else
        //     {
        //         _autoFlipperText.text = "Buy Auto-flippers";
        //         _buttonAutoFlipperImage.gameObject.SetActive(true);
        //     }
        // }

        public void BuyBuffHit()
        {
            if (PlayerDataController.PointSum < costOnGrade[FieldManager.currentField]) return;
            PlayerDataController.PointSum -= costOnGrade[FieldManager.currentField];
            //statistic
            Statistics.stats.pointSpent += costOnGrade[FieldManager.currentField];

            grade.pointOnBit[FieldManager.currentField] += 1;
            for (int _i = 0; _i < 3; _i++)
            {
                QuestManager.progress[0].progressQuest[_i]++;
            }

            QuestManager.instance.UpdateGlobalQuest();
            if (FieldManager.currentField == 0 && grade.pointOnBit[FieldManager.currentField] == 10)
                AnalyticManager.BuyTenUpgrade();
            BuffHit(FieldManager.currentField);
            ChangeHitText();
        }


        // private void BuffBonusTime(int field = 0, int countGrades = 1)
        // {
        //     if (!FieldManager.fields.isOpen[field])
        //         return;
        //     for (int _i = 0; _i < countGrades; _i++)
        //     {
        //         _costOnBonusGrade[field] = (int) (_costOnBonusGrade[field] * 1.1f);
        //     }
        // }
        //
        //
        // public void BuyBuffBonusTime()
        // {
        //     if (PlayerDataController.PointSum < _costOnBonusGrade[FieldManager.currentField]) return;
        //     PlayerDataController.PointSum -= _costOnBonusGrade[FieldManager.currentField];
        //     //statistic
        //     Statistics.stats.pointSpent += _costOnBonusGrade[FieldManager.currentField];
        //
        //     grade.bonusTime[FieldManager.currentField] += 5;
        //     BuffBonusTime(FieldManager.currentField);
        //     ChangeBonusText();
        // }
        //
        // private void BuffExpBonusTime(int field = 0, int countGrades = 1)
        // {
        //     if (!FieldManager.fields.isOpen[field])
        //         return;
        //     for (int _i = 0; _i < countGrades; _i++)
        //     {
        //         _costOnExpBonusGrade[field] = (int) (_costOnExpBonusGrade[field] * 1.1f);
        //     }
        // }
        //
        //
        // public void BuyBuffExpBonusTime()
        // {
        //     if (PlayerDataController.PointSum < _costOnExpBonusGrade[FieldManager.currentField]) return;
        //     PlayerDataController.PointSum -= _costOnExpBonusGrade[FieldManager.currentField];
        //     //statistic
        //     Statistics.stats.pointSpent += _costOnExpBonusGrade[FieldManager.currentField];
        //
        //     grade.expTime[FieldManager.currentField] += 5;
        //     BuffExpBonusTime(FieldManager.currentField);
        //     ChangeExpBonusText();
        // }

        public void BuyAutoMod(int field)
        {
            grade.autoFlippers[field] = true;
            autoMod[field] = true;
            if (field == FieldManager.currentField)
                ChallengeManager.Instance.OpenChallenges();
        }
    }

    [Serializable]
    public class CostAndGrade
    {
        public int[] pointOnBit;
        public bool[] autoFlippers;
        public int[] bonusTime;
        public int[] expTime;
        public float[] multiplyPoint;

        public CostAndGrade()
        {
            pointOnBit = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
            bonusTime = new[] {30, 30, 30, 30, 30, 30, 30, 30, 30};
            expTime = new[] {30, 30, 30, 30, 30, 30, 30, 30, 30};
            autoFlippers = new[] {false, false, false, false, false, false, false, false, false};
            multiplyPoint = new[] {1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f};
        }
    }
}