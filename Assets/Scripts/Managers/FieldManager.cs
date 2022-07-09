using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Managers
{
    public class FieldManager : MonoBehaviour
    {
        public static Fields fields;
        private static readonly int[] fieldCosts = {0, 150, 150, 150, 150, 150, 150, 150, 150};
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
        private GameObject[] _fields;

        [SerializeField]
        private GameObject[] _buyFields;

        private Coroutine _scale, _position;

        private const int CENTER_SIZE = 15;
        private const float DEFAULT_SIZE = 4f;

        public static Action openAllField;
        public static Action openOneField;

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
        }

        public void OpenAllFields()
        {
            lastField = currentField;
            openAllField?.Invoke();
            GameManager.instance.oneFieldCanvas.SetActive(false);
            GameManager.instance.bonusCanvas.SetActive(false);
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
            Debug.Log($"Start open {i} field");
            if (currentField != -1) return;
            if (!fields.isOpen[i])
            {
                BuyFields(i);
                return;
            }

            _allFieldCanvas.gameObject.SetActive(false);
            currentField = i;
            openOneField?.Invoke();
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
                GameManager.instance.bonusCanvas.SetActive(false);
                GameManager.instance.upperCanvas.SetActive(false);
                ChallengeManager.Instance._challengeCanvas.SetActive(true);
            }
            else
            {
                GameManager.instance.bonusCanvas.SetActive(true);
            }

            GameManager.instance.oneFieldCanvas.SetActive(true);
        }


        private void BuyFields(int field)
        {
            if (PlayerDataController.Gems < fieldCosts[field])
            {
                GameManager.instance.shop.SetActive(true);
                AnalyticManager.OpenDonateShop();
                return;
            }

            AnalyticManager.OpenNewField();
            PlayerDataController.Gems -= fieldCosts[field];
            fields.isOpen[field] = true;
            PlayerDataController.playerStats.lvl[field] = 1;
            PlayerDataController.LevelSum++;
            buyFields(field);
            GameManager.instance.fields[field]._allFieldElement.SetActive(true);
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
    }
}