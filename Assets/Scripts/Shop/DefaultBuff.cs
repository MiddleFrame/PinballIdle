using System;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shop
{
    public class DefaultBuff : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        private Text _costText;

        [SerializeField]
        private Text _costMultiply;

        [SerializeField]
        private Text _countMultiply;

        [SerializeField]
        private Text _pointBuffText;


        private bool _isOpen = true;

        private static DefaultBuff _instance;
        public static CostAndGrade grade;

        private const int COST_TRIPLE = 10000;

        private static readonly int[] costOnGrade =
        {
            DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST, DEFAULT_COST,
            DEFAULT_COST, DEFAULT_COST
        };

        private static readonly int[] costOnGradeMultiply =
        {
            DEFAULT_COST_MULTIPLY, DEFAULT_COST_MULTIPLY, DEFAULT_COST_MULTIPLY, DEFAULT_COST_MULTIPLY,
            DEFAULT_COST_MULTIPLY, DEFAULT_COST_MULTIPLY, DEFAULT_COST_MULTIPLY,
            DEFAULT_COST_MULTIPLY, DEFAULT_COST_MULTIPLY
        };

        private static readonly int[] chanceToUpgrade =
        {
            50, 40, 40, 35, 30, 25, 20, 15, 10
        };

        private static readonly int[] chanceToUpgradeMultiply =
        {
            50, 40, 40, 35, 30, 25, 20, 15, 10
        };

        private const int DEFAULT_COST = 100;
        private const int DEFAULT_COST_MULTIPLY = 100;
        private const int COST_AUTO_FLIPPER = 10000;

        [SerializeField]
        private GameObject _enableAutoMod;

        [SerializeField]
        private GameObject _buyTriple;

        [SerializeField]
        private GameObject _buyBuff;

        [SerializeField]
        private GameObject _buyMultiply;

        [SerializeField]
        private Text _autoFlipperText;

        [SerializeField]
        private Image _buttonImage;


        [SerializeField]
        private Image _buttonAutoFlipperImage;


        public static bool[] autoMod;

        private void Awake()
        {
            _instance = this;
            MenuController.openMenu[MenuController.Shops.UpgradeFields] += ChangeHitText;
            FieldManager.openAllField += () =>
            {
                NewElement.isBallInElement = false;
            };
            MenuController.openMenu[MenuController.Shops.UpgradeFields] += ChangeHitTextMultiply;
            MenuController.openMenu[MenuController.Shops.UpgradeFields] += OpenAutoMod;
            MenuController.openMenu[MenuController.Shops.UpgradeFields] += () =>
            {
                _buyTriple.SetActive(!grade.triple[FieldManager.currentField]);
            };
        }

        private void Start()
        {
            autoMod = new bool[grade.autoFlippers.Length];

            for (int _i = 0; _i < FieldManager.fields.isOpen.Length; _i++)
            {
                if (!FieldManager.fields.isOpen[_i]) continue;
                BuffHit(_i, grade.tryPointOnBit[_i] - 1);
                BuffMultiply(_i, grade.tryMultiply[_i] - 1);
                if (grade.autoFlippers[_i])
                {
                    _enableAutoMod.SetActive(false);
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

        private void BuffMultiply(int field = 0, int countGrades = 1)
        {
            if (!FieldManager.fields.isOpen[field])
                return;
            for (int _i = 0; _i < countGrades; _i++)
            {
                costOnGradeMultiply[field] = (int) (costOnGradeMultiply[field] * 1.1f);
            }
        }

        private void ChangeHitTextMultiply()
        {
            _costMultiply.text = GameManager.NormalSum(costOnGradeMultiply[FieldManager.currentField]);
            _countMultiply.text =
                "Multiple point on click " + grade.multiply[FieldManager.currentField];
        }

        public void BuyBuffMultiply()
        {
            if (PlayerDataController.PointSum < costOnGradeMultiply[FieldManager.currentField]) return;
            PlayerDataController.PointSum -= costOnGradeMultiply[FieldManager.currentField];
            //statistic
            Statistics.stats.pointSpent += costOnGradeMultiply[FieldManager.currentField];
            BuffMultiply(FieldManager.currentField);
            grade.tryMultiply[FieldManager.currentField]++;
            if (Random.Range(0, 100) <
                chanceToUpgradeMultiply[(int) ((grade.multiply[FieldManager.currentField] - 2) / 0.5f)])
            {
                grade.multiply[FieldManager.currentField] += 0.5f;
                if (Math.Abs(grade.multiply[FieldManager.currentField] - 7f) < 0.1f)
                {
                    _buyMultiply.SetActive(false);
                }
            }

            ChangeHitTextMultiply();
        }

        private void OpenAutoMod()
        {
            if (grade.autoFlippers[FieldManager.currentField])
            {
                _autoFlipperText.text = "Auto-flippers";
                _buttonAutoFlipperImage.gameObject.SetActive(false);
            }
            else
            {
                _autoFlipperText.text = "Buy Auto-flippers";
                _buttonAutoFlipperImage.gameObject.SetActive(true);
            }
        }

        public void BuyBuffHit()
        {
            if (PlayerDataController.PointSum < costOnGrade[FieldManager.currentField]) return;
            PlayerDataController.PointSum -= costOnGrade[FieldManager.currentField];
            //statistic
            Statistics.stats.pointSpent += costOnGrade[FieldManager.currentField];
            BuffHit(FieldManager.currentField);
            grade.tryPointOnBit[FieldManager.currentField]++;
            if (Random.Range(0, 100) < chanceToUpgrade[grade.pointOnBit[FieldManager.currentField] - 1])
            {
                grade.pointOnBit[FieldManager.currentField] += 1;
                if (grade.pointOnBit[FieldManager.currentField] == 10)
                {
                    _buyBuff.SetActive(false);
                }

                for (int _i = 0; _i < 3; _i++)
                {
                    QuestManager.progress[0].progressQuest[_i]++;
                }

                QuestManager.instance.UpdateGlobalQuest();
            }

            ChangeHitText();
        }


        private void buyAutoMod()
        {
            grade.autoFlippers[FieldManager.currentField] = true;
            autoMod[FieldManager.currentField] = true;
            OpenAutoMod();
        }

        public void BuyAutoMod()
        {
            if (PlayerDataController.PointSum < COST_AUTO_FLIPPER) return;
            PlayerDataController.PointSum -= COST_AUTO_FLIPPER;
            buyAutoMod();
        }

        public void EnableAutoMod()
        {
            autoMod[FieldManager.currentField] = true;
            _enableAutoMod.SetActive(false);
        }

        public void DisableAutoMod()
        {
            autoMod[FieldManager.currentField] = false;
            _enableAutoMod.SetActive(true);
        }

        public void BuyTriple()
        {
            if (PlayerDataController.PointSum < COST_TRIPLE) return;
            PlayerDataController.PointSum -= COST_TRIPLE;
            grade.triple[FieldManager.currentField] = true;
            _buyTriple.SetActive(false);
        }
    }

    [Serializable]
    public class CostAndGrade
    {
        public float[] multiply;
        public int[] tryMultiply;
        public int[] pointOnBit;
        public int[] tryPointOnBit;
        public bool[] autoFlippers;
        public bool[] stopper;
        public bool[] triple;
        public int[] bonusTime;
        public int[] expTime;
        public float[] multiplyPoint;

        public CostAndGrade()
        {
            pointOnBit = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
            tryPointOnBit = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
            multiply = new[] {2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f};
            tryMultiply = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
            bonusTime = new[] {30, 30, 30, 30, 30, 30, 30, 30, 30};
            expTime = new[] {30, 30, 30, 30, 30, 30, 30, 30, 30};
            autoFlippers = new[] {false, false, false, false, false, false, false, false, false};
            stopper = new[] {false, false, false, false, false, false, false, false, false};
            triple = new[] {false, false, false, false, false, false, false, false, false};
            multiplyPoint = new[] {1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f};
        }
    }
}