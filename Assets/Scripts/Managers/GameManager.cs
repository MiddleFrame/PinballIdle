using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public Field[] fields;
        public GameObject shop;

        public Sprite _lockedSprite;

        public Sprite defaultShadowBall;
        public Sprite goldShadowBall;
        public Sprite _unlockedSprite;
        public Teleport[] spawnPoints;
        public GameObject oneFieldCanvas;
        public GameObject upperCanvas;
        public GameObject bonusCanvas;
        public Color tripleColor;
        public Color defaultColor;
        public static Gradient[] trails;
        public static Color[] ballColor;
        [SerializeField]
        private  Gradient[] _trails;
        [SerializeField]
        private  Color[] _ballColor;
        [SerializeField]
        private GameObject text;

        [SerializeField]
        private Image _deleteProgressImage;
        private static Image deleteProgressImage;

        public Sprite _lockFunctionSprite;

        private int _launchTheGame
        {
            get => PlayerPrefs.GetInt("launchTheGame",0);
            set => PlayerPrefs.SetInt("launchTheGame",value);
        }
        public static GameObject Text => instance.text;


        private void Awake()
        {
            instance = this;
            LetsScript.isCompetitive = false;
            Vibration.Init();
            deleteProgressImage = _deleteProgressImage;
        }

        private void Start()
        {
            trails = new Gradient[_trails.Length];
            for (int _i = 0; _i < _trails.Length; _i++)
            {
                trails[_i] = _trails[_i];
            } 
            ballColor = new Color[_ballColor.Length];
            for (int _i = 0; _i < _ballColor.Length; _i++)
            {
                ballColor[_i] = _ballColor[_i];
            }
            Text.GetComponent<MeshRenderer>().sortingOrder = 3;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            _launchTheGame++;
            if (_launchTheGame%3 == 0 && _launchTheGame >0)
            {
                Debug.Log("tryReview");
                StartCoroutine(Review.OpenReview());
            }
        }


        public static string NormalSum(long p)
        {
            string _res;
            if (p + 1 >= 1000 && p < 10000)
                _res = Math.Round((p) / 1000.0, 2, MidpointRounding.AwayFromZero) + "K";
            else if (p + 1 >= 10000 && p < 100000)
                _res = Math.Round((p) / 1000.0, 1, MidpointRounding.AwayFromZero) + "K";
            else if (p + 1 >= 100000 && p < 1000000)
                _res = (int) (p / 1000.0) + "K";
            else if (p + 1 >= 1000000 && p < 10000000)
                _res = Math.Round(p / 1000000.0, 2, MidpointRounding.AwayFromZero) + "M";
            else if (p + 1 >= 10000000 && p < 100000000)
                _res = Math.Round(p / 1000000.0, 1, MidpointRounding.AwayFromZero) + "M";
            else if (p + 1 >= 100000000 && p < 1000000000)
                _res = Math.Round(p / 1000000.0) + "M";
            else if (p + 1 >= 1000000000 && p < 10000000000)
                _res = Math.Round(p / 1000000000.0, 2, MidpointRounding.AwayFromZero) + "B";
            else if (p + 1 >= 10000000000 && p < 100000000000)
                _res = Math.Round(p / 1000000000.0, 1, MidpointRounding.AwayFromZero) + "B";
            else if (p + 1 >= 100000000000)
                _res = Math.Round(p / 1000000000.0) + "B";
            else
                _res = "" + p;
            return _res;
        }


        public static void TextDown(GameObject tx)
        {
                tx.transform.position -= new Vector3(0, 0.08f, 0);
            
        }

        private static Coroutine _deleteCoroutine;
        public static void DeleteAllProgress()
        {
            if (_deleteCoroutine != null)
            {
                instance.StopCoroutine(_deleteCoroutine);
                _deleteCoroutine = null;
                deleteProgressImage.fillAmount = 0;
            }
            _deleteCoroutine = instance.StartCoroutine(deleteProgress());
        }
        public static void StopDeleteAllProgress()
        {
            if (_deleteCoroutine == null) return;
            instance.StopCoroutine(_deleteCoroutine);
            _deleteCoroutine = null;
            deleteProgressImage.fillAmount = 0;
        }

        private static IEnumerator deleteProgress()
        {
            while (deleteProgressImage.fillAmount<1)
            {
                deleteProgressImage.fillAmount += Time.deltaTime;
                yield return null;
            }
            PlayerPrefs.DeleteAll();
            RestartAndroid();
        }
        private static void RestartAndroid() {
            if (Application.isEditor) return;

            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                const int kIntent_FLAG_ACTIVITY_CLEAR_TASK = 0x00008000;
                const int kIntent_FLAG_ACTIVITY_NEW_TASK = 0x10000000;

                var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                var pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
                var intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);

                intent.Call<AndroidJavaObject>("setFlags", kIntent_FLAG_ACTIVITY_NEW_TASK | kIntent_FLAG_ACTIVITY_CLEAR_TASK);
                currentActivity.Call("startActivity", intent);
                currentActivity.Call("finish");
                var process = new AndroidJavaClass("android.os.Process");
                int pid = process.CallStatic<int>("myPid");
                process.CallStatic("killProcess", pid);
            }
        }

        public static void TextUp(GameObject tx)
        {
                tx.transform.position += new Vector3(0, 0.08f, 0);
        }
        
    }
}