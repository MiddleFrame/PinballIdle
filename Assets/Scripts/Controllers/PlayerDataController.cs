using System;
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
        private Image[] _exp;

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
            /*FieldManager.openAllField += () =>
            {
                _keyPanel.SetActive(true);
            };
            FieldManager.openOneField += () =>
            {
                _keyPanel.SetActive(false);
            };*/
            FieldManager.openOneField += changeFillAmount;
            FieldManager.openOneField += changeLevelText;
            _instance = this;
        }

        private void Start()
        {
            Debug.Log("Sum of levels: " + LevelSum);
            PointSum = playerStats.pointSum;
            Gems = playerStats.gems;
            //Key = playerStats.key;
            changeFillAmount();
            changeLevelText();
        }

        private static void changeFieldsLevelText()
        {
            for (int _i = 0; _i < FieldsFactory.FieldsCount; _i++)
            {
                FieldsFactory.GetField(_i).levelText.text = $"lvl {playerStats.lvl[_i]-1}";
                FieldsFactory.GetField(_i).ballsText.text = $"x{ChallengeManager.progress.balls[_i] + 1}";
            }
        }

        public static void AddExp(int field, int exp)
        {
            if (playerStats.lvl[field] > 10) return;
            playerStats.exp[field] += exp;
            if (playerStats.exp[field] >= 100 * playerStats.lvl[field])
            {
                playerStats.lvl[field]++;
                for (int _i = 6; _i < QuestManager.progress[0].isComplete.Length; _i++)
                {
                    QuestManager.progress[0].progressQuest[_i]++;
                }
                QuestManager.instance.UpdateGlobalQuest();
                LevelSum++;
                playerStats.exp[field] = 0;
                QuestManager.instance.CheckLevelQuest(field);
                if (field == FieldManager.currentField)
                {
                    _instance._lvlOnLvlUpPanel.text = playerStats.lvl[field].ToString();
                    _instance._lvlOnLvlUpPanelMultiply.text = "x" + (1 + 0.1f * (playerStats.lvl[field] - 1));
                    _instance._lvlUpPanel.SetActive(true);
                    _instance._lvlMain.text = (playerStats.lvl[field] - 1).ToString();
                    _instance._lvlBuff.text =
                        $"x {1 + 0.1f * (playerStats.lvl[field] - 1) * RewardPoint.hitMultiply[field]}";
                }
            }

            if (field == FieldManager.currentField)
                changeFillAmount();
        }

        public void CloseLevelPanel()
        {
            _lvlUpPanel.SetActive(false);
            if (FieldManager.currentField != 0 || playerStats.lvl[0] != 2) return;
            MenuController.instance.OpenShop((int)MenuController.Shops.AllField);
            FindObjectOfType<FieldManager>().OpenAllFields();
        }

        private static void changeFillAmount()
        {
            for (int _i = 0; _i < _instance._exp.Length; _i++)
            {
                _instance._exp[_i].fillAmount = _i > playerStats.lvl[FieldManager.currentField]-1 ? 0 : 1;
            }

            if (playerStats.lvl[FieldManager.currentField]-1 >= _instance._exp.Length ) return;
            _instance._exp[playerStats.lvl[FieldManager.currentField]-1].fillAmount =
                playerStats.exp[FieldManager.currentField] /
                (200f * playerStats.lvl[FieldManager.currentField]);
        }

        private static void changeLevelText()
        {
            _instance._lvlMain.text = (playerStats.lvl[FieldManager.currentField] - 1).ToString();
            _instance._lvlBuff.text =
                $"x {1 + 0.1f * (playerStats.lvl[FieldManager.currentField] - 1) * RewardPoint.hitMultiply[FieldManager.currentField]}";
        }

        public void DeleteProgress()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("MainFieldsScene");
        }
    }


    public class PlayerStats
    {
        public long pointSum;
        public int gems;
        public int key=0;
        public int[] lvl = {1, 0, 0, 0, 0, 0, 0, 0, 0};
        public int[] exp = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    }
}