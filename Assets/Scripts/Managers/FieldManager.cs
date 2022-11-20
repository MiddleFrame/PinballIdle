using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Managers
{
    public class FieldManager : MonoBehaviour
    {
        public static FieldManager instance;
        public static Fields fields;
        private static readonly int[] fieldCosts = {0, 150, 300, 450, 600, 750, 900, 1050, 1200};
        public static int currentField;
        private static int lastField;

        [Header("Camera"), SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        private Transform _camera;

        [Space(20), SerializeField]
        private Vector3 _centerPosition;

        [SerializeField]
        private Vector3[] _fieldsPosition;


        [SerializeField]
        private Canvas _allFieldCanvas;

        [SerializeField]
        private GameObject _backButton;
        
        [SerializeField]
        private GameObject[] _fields;
        [SerializeField]
        private GameObject[] _areas;

        [SerializeField]
        private GameObject[] _buyFields;
        [SerializeField]
        private GameObject[] _buyFieldsButton;

        private Coroutine _scale, _position;

        private const int CENTER_SIZE = 17;
        private const float DEFAULT_SIZE = 5f;

        public static Action openAllField;
        public static Action openOneField;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            currentField = 0;
            Debug.Log("Fields: " + JsonUtility.ToJson(fields));
            for (int _i = 0; _i < fields.isOpen.Length; _i++)
            {
                if (fields.isOpen[_i])
                {
                    buyFields(_i);
                }
            }  
            for (int _i = 0; _i < fields.isAreaOpen.Length; _i++)
            {
                if (fields.isAreaOpen[_i])
                {
                    _areas[_i].SetActive(false);
                }
            }

            openAllField += () =>
            {
                int _flag = 0;
                for (int _i = 0; _i < fields.isOpen.Length; _i++)
                {
                    if (fields.isOpen[_i] && _i < fields.isOpen.Length - 1 && !fields.isOpen[_i + 1])
                    {
                        _buyFieldsButton[_i+1].SetActive(_i+1 < 3 || fields.isAreaOpen[(_i+1)/3-1]);
                        _flag = _i+1;
                    }

                    if (_i > _flag)
                    {
                        _buyFieldsButton[_i].SetActive(false);
                    }
                }
                
            };
        }

        public void OpenAllFields()
        {
            lastField = currentField;
            openAllField?.Invoke();
            GameManager.instance.oneFieldCanvas.SetActive(false);
            GameManager.instance.triggerCanvas.SetActive(false);
            // GameManager.instance.bonusCanvas.SetActive(false);
            GameManager.instance.upperCanvas.SetActive(true);
            ChallengeManager.Instance._challengeCanvas.SetActive(false);
            if (_scale != null)
            {
                StopCoroutine(_scale);
                _scale = null;
                StopCoroutine(_position);
                _position = null;
            }

            _position = StartCoroutine(moveCamera(_centerPosition));
            _scale = StartCoroutine(scaleCamera(true));
        }

        public void OpenLastField()
        {
            OpenSomeField(lastField);
        }

        public void OpenSomeField(int i)
        {
            if (currentField != -1) return;
            if (!fields.isOpen[i]) return;
            Debug.Log($"Start open {i} field");
            _allFieldCanvas.gameObject.SetActive(false);
            currentField = i;
            openOneField?.Invoke();
            _backButton.SetActive(false);
            if (_scale != null)
            {
                StopCoroutine(_scale);
                _scale = null;
                StopCoroutine(_position);
                _position = null;
            }

            _position = StartCoroutine(moveCamera(_fieldsPosition[i]));
            _scale = StartCoroutine(scaleCamera(false));
        }

        public void UnlockArea(int area)
        {
            if (PlayerDataController.playerStats.key < 3)
            {
                MenuController.instance.OpenShop(1);
                return;
            }
            PlayerDataController.playerStats.key -= 3;
            _areas[area].SetActive(false);
            fields.isAreaOpen[area] = true;
            openAllField?.Invoke();
        }

        private IEnumerator moveCamera(Vector3 lastPos)
        {
            var _start = _camera.transform.position;

            var _progress = 0f;
            Debug.Log($"Start moving with progress {_progress}");
            while (_progress <= 0.99f)
            {
                _camera.transform.position = Vector3.Lerp(_start, lastPos, _progress);
                _progress += 1.818f * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _camera.transform.position = lastPos;
            Debug.Log($"Position finish. progress: {_progress}");
        }

        private IEnumerator scaleCamera(bool isCenter)
        {
            Debug.Log($"Start scaling with scale {_mainCamera.orthographicSize}");
            var _scaler = isCenter ? 1 : -1;
            if (isCenter)
            {
                currentField = -1;
            }

            while (isCenter
                       ? _mainCamera.orthographicSize < CENTER_SIZE - 0.01f
                       : _mainCamera.orthographicSize > DEFAULT_SIZE + 0.01f)
            {
                _mainCamera.orthographicSize += _scaler * 20f * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Debug.Log($"Finish scaling with scale {_mainCamera.orthographicSize}");

            _mainCamera.orthographicSize = isCenter ? CENTER_SIZE : DEFAULT_SIZE;
            _allFieldCanvas.gameObject.SetActive(isCenter);

            if (isCenter) yield break;

            if (ChallengeManager.IsStartChallenge[currentField])
            {
                // GameManager.instance.bonusCanvas.SetActive(false);
                GameManager.instance.upperCanvas.SetActive(false);
                ChallengeManager.Instance._challengeCanvas.SetActive(true);
                ChallengeManager.Instance._level.SetActive(false);
            }
            else
            {
                ChallengeManager.Instance._level.SetActive(true);
                // GameManager.instance.bonusCanvas.SetActive(true);
            }

            GameManager.instance.oneFieldCanvas.SetActive(true);
            GameManager.instance.triggerCanvas.SetActive(true);
        }


        public void BuyFields(int field)
        {
            if (PlayerDataController.Gems < fieldCosts[field])
            {
                MenuController.instance.OpenShop((int)MenuController.Shops.Shop);
                AnalyticManager.OpenDonateShop();
                return;
            }

            AnalyticManager.OpenNewField(field);
            PlayerDataController.Gems -= fieldCosts[field];
            fields.isOpen[field] = true;
            PlayerDataController.playerStats.lvl[field] = 1;
            PlayerDataController.LevelSum++;
            buyFields(field);
            GameManager.instance.fields[field]._allFieldElement.SetActive(true);
            openAllField.Invoke();
        }

        private void buyFields(int field)
        {
            _buyFields[field].SetActive(false);
            _fields[field].SetActive(true);
            
        }
        
    }


    public class Fields
    {
        public bool[] isOpen = {true, false, false, false, false, false, false, false, false};
        public bool[] isAreaOpen = {false, false};
    }
}