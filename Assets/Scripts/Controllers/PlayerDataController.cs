using System;
using System.Linq;
using Managers;
using Shop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class PlayerDataController : MonoBehaviour
    {
        public static PlayerStats playerStats;
        private static PlayerDataController _instance;

        [SerializeField]
        private GameObject _lvlUpPanel;

        [SerializeField]
        private Text _gems;

        public static int Gems
        {
            get => playerStats.gems;
            set
            {
                playerStats.gems = value;
                _instance._gems.text = GameManager.NormalSum(playerStats.gems);
            }
        }

        [SerializeField]
        private Image _exp;

        [SerializeField]
        private Text _lvlMain;

        [SerializeField]
        private Text _lvlOnLvlUpPanel;

        [SerializeField]
        private Text _lvlOnLvlUpPanelMultiply;

        [SerializeField]
        private Text _lvlBuff;

        private static int _levelSum;

        public static int LevelSum
        {
            get => _levelSum;


            set
            {
                _levelSum = value;
                PigMoneybox.MaxPoints = 10000 + _levelSum * 5000;
                PigMoneybox.NextClaim = DateTime.Now.AddMinutes((float) PigMoneybox.MaxPoints /
                                                                (PlayerDataController.LevelSum * 15));
            }
        }

        [SerializeField]
        private Text _pointSumText;

        public static long PointSum
        {
            get => playerStats.pointSum;
            set
            {
                playerStats.pointSum = value;
                _instance._pointSumText.text = GameManager.NormalSum(playerStats.pointSum);
            }
        }

        private void Awake()
        {
            FieldManager.openAllField += changeFieldsLevelText;
            FieldManager.openOneField += changeFillAmount;
            FieldManager.openOneField += changeLevelText;
            _instance = this;
        }

        private void Start()
        {
            _levelSum = playerStats.lvl.Sum();
            PigMoneybox.MaxPoints = 5000 + _levelSum * 1000;
            Debug.Log("Sum of levels: " + LevelSum);
            PointSum = playerStats.pointSum;
            Gems = playerStats.gems;
            changeFillAmount();
            changeLevelText();
        }

        private static void changeFieldsLevelText()
        {
            for (int _i = 0; _i < GameManager.instance.fields.Length; _i++)
            {
                GameManager.instance.fields[_i].levelText.text = $"lvl {playerStats.lvl[_i]}";
                GameManager.instance.fields[_i].ballsText.text = $"x{ChallengeManager.progress.balls[_i]+1}";
            }
        }

        public static void AddExp(int field, int exp)
        {
            playerStats.exp[field] += exp;
            if (playerStats.exp[field] >= 200 * playerStats.lvl[field])
            {
                playerStats.lvl[field]++;
                LevelSum++;
                playerStats.exp[field] = 0;
                if (field == FieldManager.currentField)
                {
                    _instance._lvlOnLvlUpPanel.text = playerStats.lvl[field].ToString();
                    _instance._lvlOnLvlUpPanelMultiply.text = "x" + playerStats.lvl[field];
                    _instance._lvlUpPanel.SetActive(true);
                    _instance._lvlMain.text = playerStats.lvl[field].ToString();
                    _instance._lvlBuff.text = $"x {playerStats.lvl[field] * RewardPoint.hitMultiply[field]}";
                }
            }

            if (field == FieldManager.currentField)
                changeFillAmount();
        }

        public void CloseLevelPanel()
        {
            _lvlUpPanel.SetActive(false);
            if (FieldManager.currentField != 0 || playerStats.lvl[0] != 2) return;
            MenuController.instance.OpenShop(-2);
            FindObjectOfType<FieldManager>().OpenAllFields();
        }

        private static void changeFillAmount()
        {
            _instance._exp.fillAmount = playerStats.exp[FieldManager.currentField] /
                                        (200f * playerStats.lvl[FieldManager.currentField]);
        }

        private static void changeLevelText()
        {
            _instance._lvlMain.text = playerStats.lvl[FieldManager.currentField].ToString();
            _instance._lvlBuff.text =
                $"x {playerStats.lvl[FieldManager.currentField] * RewardPoint.hitMultiply[FieldManager.currentField]}";
        }

        public void DeleteProgress()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
    }


    public class PlayerStats
    {
        public long pointSum;
        public int gems;
        public int[] lvl = {1, 0, 0, 0, 0, 0, 0, 0, 0};
        public int[] exp = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    }
}