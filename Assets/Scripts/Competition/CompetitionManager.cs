using System.Collections;
using System.Globalization;
using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Competition
{
    public class CompetitionManager : MonoBehaviour
    {
        public static int PlayerPoint => point[0];
        private static int[] point;
        private const int MAX_SCORE = 10000;
        public static CompetitionManager instance;
        public static int[] balls;
        public static int[] upgrades;
        public static bool[] isUpgradeBuff;
        public static int winner;
        public static bool[] isWinners;

        public static Pattern[] patterns;
        private static int[] currentPattern;

        [SerializeField]
        private GameObject[] _completeWindow;
        
        public CompetitionTeleport[] _teleports;

        [SerializeField]
        private GameObject[] _trapAnim;


        [SerializeField]
        private GameObject[] _strokes;

        [SerializeField]
        private Image[] _progressBars;

        [SerializeField]
        private Text[] _progressScores;
        [SerializeField]
        private Text _progress;

        [SerializeField]
        private GameObject text;

        public static GameObject Text => instance.text;
        public static bool[] isBuff = {false, false, false, false, false, false, false, false, false};

        void Awake()
        {
            patterns = new Pattern[8];
            for (int _i = 0; _i < patterns.Length; _i++)
            {
                patterns[_i] = new Pattern();
            }

            isWinners = new bool[9];
            winner = 0;
            var _camera = Camera.main;
            float _ratio = 1f * Screen.height / Screen.width;
            if (_camera != null) _camera.transform.position = new Vector3(0, -11.7f, -24 - 2 * _ratio);
            balls = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0};
            isUpgradeBuff = new[] {false, false, false, false, false, false, false, false, false};
            upgrades = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
            point = new int[9]; //{9997, 9997, 9997, 9997, 9997, 9997, 9997, 9997, 9997};
            instance = this;
            FieldManager.currentField = 0;
            LetsScript.isCompetitive = true;
            currentPattern = new int[8];

            for (int _i = 1; _i < isBuff.Length; _i++)
            {
                isBuff[_i] = Random.Range(0, 1.0f) > 0.4f;
            }

            if (!Setting.settings.sound)
            {
                AudioListener.volume = 0;
            }
        }

        public static void AddPoint(int score, int field)
        {
            if (isWinners[field]) return;
            if (point[field] + score < 0)
                point[field] = 0;
            else
                point[field] += score;
            instance._progressBars[field].fillAmount = (float) point[field] / MAX_SCORE;
            instance._progressScores[field].text = ((float) point[field] / MAX_SCORE)
                .ToString("#0%", CultureInfo.CreateSpecificCulture("es-ES"));

            if (point[field] >= MAX_SCORE)
            {
                isWinners[field] = true;
                winner++;
                instance._completeWindow[field].SetActive(true);
            }
            if (field == 0)
            {
                instance._progressScores[field].text =
                    point[field].ToString("##,##0", CultureInfo.CreateSpecificCulture("es-ES")) + "/" +
                    MAX_SCORE.ToString("##,##0", CultureInfo.CreateSpecificCulture("es-ES"));
                instance._progress.text = "Progress (" + ((float) point[field] / MAX_SCORE)
                    .ToString("#0%", CultureInfo.CreateSpecificCulture("es-ES"))+")";;
            }
            else
            {
                if (point[field] > patterns[field - 1].cost[patterns[field - 1].patterns[currentPattern[field - 1]]])
                {
                    var _currentField = field - 1;
                    switch (patterns[_currentField].patterns[currentPattern[_currentField]])
                    {
                        case 0:
                            BuyUpgrade(field,
                                patterns[_currentField]
                                    .cost[patterns[_currentField].patterns[currentPattern[_currentField]]]);
                            patterns[_currentField]
                                .cost[patterns[_currentField].patterns[currentPattern[_currentField]]] += 50;
                            break;
                        case 1:
                            if (isUpgradeBuff[field]) return;
                            BuyUpgradeBuff(field,
                                patterns[_currentField]
                                    .cost[patterns[_currentField].patterns[currentPattern[_currentField]]]);
                            patterns[_currentField]
                                .cost[patterns[_currentField].patterns[currentPattern[_currentField]]] *= 2;
                            break;
                        case 2:
                            BuyBall(field,
                                patterns[_currentField]
                                    .cost[patterns[_currentField].patterns[currentPattern[_currentField]]]);
                            patterns[_currentField]
                                .cost[patterns[_currentField].patterns[currentPattern[_currentField]]] += 200;
                            break;
                        case 3:
                            BuyTrap(field,
                                patterns[_currentField]
                                    .cost[patterns[_currentField].patterns[currentPattern[_currentField]]]);
                            break;
                    }

                    currentPattern[field - 1]++;
                }
            }

           
        }

        public static void ElementUp(GameObject tx)
        {
            tx.transform.position += new Vector3(0, 0.01f, 0);
        }

        public static void ElementDown(GameObject tx)
        {
            tx.transform.position -= new Vector3(0, 0.01f, 0);
        }


        public static void BuyUpgrade(int field, int cost)
        {
            upgrades[field]+=1;
            AddPoint(-cost, field);
        }

        public static void BuyBall(int field, int cost)
        {
            balls[field]++;

            AddPoint(-cost, field);
        }

        public static void BuyUpgradeBuff(int field, int cost)
        {
            isUpgradeBuff[field] = true;
            instance._strokes[field].SetActive(true);
            instance.StartCoroutine(timerBuff(field));

            AddPoint(-cost, field);
        }

        private static IEnumerator timerBuff(int field)
        {
            for (int _i = 0; _i < 30; _i++)
            {
                yield return new WaitForSeconds(1f);
            }

            instance._strokes[field].SetActive(false);
            isUpgradeBuff[field] = false;
        }

        public static void BuyTrap(int field, int cost)
        {
            AddPoint(-cost, field);
            for (int _i = 0; _i < 9; _i++)
            {
                if (_i == field || isWinners[_i]) continue;
                instance.StartCoroutine(AwaitTrap(_i, cost));
                if (!instance._trapAnim[_i].activeSelf)
                    instance._trapAnim[_i].SetActive(true);
            }
        }

        private static IEnumerator AwaitTrap(int field, int cost)
        {
            yield return new WaitForSeconds(0.5f);
            AddPoint(-cost, field);
        }

        public static void GetPrice()
        {
            switch (winner)
            {
                case 1:
                    PlayerDataController.playerStats.gems += 100;
                    break;
                case 2:
                    PlayerDataController.playerStats.gems += 75;
                    break;
                case 3:
                    PlayerDataController.playerStats.gems += 50;
                    break;
            }
        }

        public static void EndCompetition()
        {
            GetPrice();
            FieldManager.openAllField = null;
            FieldManager.openOneField = null;
            for (int _i = 0; _i < MenuController.shopOpen.Length; _i++)
                MenuController.shopOpen[_i] = null;
            SceneManager.LoadScene(0);
            AdManager.ShowInterstitial();
        }
    }


    public class Pattern
    {
        public int[] patterns;

        public int[] cost = {50, 200, 500, 1500, 99999};

        public Pattern()
        {
            patterns = new int[Random.Range(12, 23)];
            patterns[0] = 0;
            patterns[1] = Random.Range(0, 1f) > 0.7f ? 2 : 0;
            patterns[2] = 0;
            patterns[3] = Random.Range(0, 1f) > 0.6f ? 2 : 0;
            patterns[4] = Random.Range(0, 1f) > 0.7f ? 2 : 0;

            for (int _i = 5; _i < patterns.Length - 6; _i++)
            {
                patterns[_i] = Random.Range(0, 1f) > 0.9f ? 3 :
                    Random.Range(0, 1f) > 0.6f ? 2 :
                    Random.Range(0, 1f) > 0.4f ? 1 : 0;
            }

            patterns[patterns.Length - 6] = Random.Range(0, 1f) > 0.7f ? 3 : 1;
            patterns[patterns.Length - 5] = 1;
            patterns[patterns.Length - 4] = Random.Range(0, 1f) > 0.9f ? 3 : 1;
            patterns[patterns.Length - 3] = 1;
            patterns[patterns.Length - 2] = 1;
            patterns[patterns.Length - 1] = 4;
        }
    }
}