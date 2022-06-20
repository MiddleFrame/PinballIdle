using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Competition
{
    public class CompetitionManager : MonoBehaviour
    {
        
        public static int PlayerPoint => point[0];
        private static int[] point;
        private static int maxScore=10000;
        private static CompetitionManager instance;
        public static int[] balls;
        public static int[] upgrades;
         public static bool[] isUpgradeBuff;
         public static int winer;
         [SerializeField]
         private GameObject[] _completeWindow;
         [SerializeField]
         private GameObject[] _trapAnim;
         [SerializeField]
         private FlipperLeftCompetition[] _flippers;
         [SerializeField]
         private GameObject[] _strokes;
        [SerializeField]
        private Image[] _progressBars;
        [SerializeField]
        private Text[] _progressScores;

        [SerializeField]
        private GameObject text;
        public static GameObject Text => instance.text;
        public static bool[] isBuff = {false,false,false,false,false,false,false,false,false};
        void Awake()
        {
            winer = 0;
            var _camera = Camera.main;
            float _ratio = 1f * Screen.height / Screen.width;
            if (_camera != null) _camera.transform.position =new Vector3(0,-9.7f, -24-2*_ratio);
            balls = new [] {0, 0, 0, 0, 0, 0, 0, 0, 0};
            isUpgradeBuff = new [] {false,false,false,false,false,false,false,false,false};
            upgrades = new [] {1, 1, 1, 1, 1, 1, 1, 1, 1};
            point = new int[9];
            instance = this;
            FieldManager.currentField = 0;
            LetsScript.isCompetitive = true;
            if (!Setting.settings.sound)
            {
                AudioListener.volume = 0;
            }
        }

        public static void AddPoint(int score, int field)
        {
            if (point[field] + score < 0)
                point[field] = 0;
            point[field] += score;
            instance._progressBars[field].fillAmount = (float)point[field]/maxScore;
            instance._progressScores[field].text = $"{Mathf.Floor((float)point[field]/maxScore*100)}%";
            if (point[field] >= maxScore)
            {
                winer++;
                instance._completeWindow[field].SetActive(true);
                instance._flippers[field].Player = true;
                instance._flippers[9+field].Player = true;
            }
            if (field == 0)
            {
               
                instance._progressScores[field].text = point[field].ToString();
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
            upgrades[field]++;
            AddPoint(-cost, field);
        }
        
        public static void BuyBall(int field,int cost)
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
                if (_i == field) continue;
                instance.StartCoroutine(AwaitTrap(_i, cost));
                if(!instance._trapAnim[_i].activeSelf)
                    instance._trapAnim[_i].SetActive(true);
            }
        }

        private static IEnumerator AwaitTrap(int field, int cost)
        {
            yield return new WaitForSeconds(0.5f);
            AddPoint(-cost, field);
        }
    }
}
