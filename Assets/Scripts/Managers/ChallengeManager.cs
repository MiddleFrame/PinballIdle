using System.Collections;
using Controllers;
using Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ChallengeManager : MonoBehaviour
    {
        public static ChallengeProgress progress;

        [SerializeField]
        private Image _challengeButton;

        [SerializeField]
        private Sprite _openChallenge;

        [SerializeField]
        private GameObject[] _challengePanel;

        [SerializeField]
        private Text[] _progressTexts;

        [SerializeField]
        private Image[] _progressFills;

        [SerializeField]
        private GameObject _newChallengePanel;

        [SerializeField]
        private GameObject _completePanel;


        public GameObject _challengeCanvas;

        [SerializeField]
        private Image _buyBallsImage;

        [SerializeField]
        private Text _costBall;

        [SerializeField]
        private Text _countBallsText;

        [SerializeField]
        private Image _startChallengeImage;

        [SerializeField]
        private Text _startChallengeText;

        [SerializeField]
        private Text _getRewardText;

        [SerializeField]
        private Text _challengeText;

        [SerializeField]
        private Text _challengeCountText;

        [SerializeField]
        private Text _challengeProgressText;

        [SerializeField]
        private Image _challengeProgress;

        private bool _isMaximum;

        public static ChallengeManager Instance { get; private set; }

        public static readonly bool[] IsStartChallenge =
            {false, false, false, false, false, false, false, false, false};


        private readonly string[] _challenges =
        {
            ".",
            " when all obstacles are halved.",
            "provided that with a 15% chance of losing point.",
            "provided that after five seconds the ball stops scoring.",
            "if all obstacles are halved, with a 15% chance of losing points and after five seconds the ball stops scoring."
        };

        private void Awake()
        {
            Instance = this;
            FieldManager.openAllField += () =>
            {
                for (int _field = 0; _field < 9; _field++)
                {
                    if (!IsStartChallenge[_field]) continue;
                    _progressTexts[_field].text =
                        $"{Mathf.Floor(progress.currentProgressChallenge[_field] / ((progress.countCompleteChallenge[_field] + 1) * 1000f) * 100)}%";
                    _progressFills[_field].fillAmount =
                        progress.currentProgressChallenge[_field] /
                        ((progress.countCompleteChallenge[_field] + 1) * 1000f);
                }
            };
            FieldManager.openOneField += () =>
            {
                if (DefaultBuff.grade.autoFlippers[FieldManager.currentField])
                    OpenChallenges();
                else
                    CloseChallenges();
                switch (IsStartChallenge[FieldManager.currentField])
                {
                    case false when _progressTexts[FieldManager.currentField].text == "COMPLETE":
                        getReward();
                        break;
                    case true:
                        ChangeTextAndFill();
                        break;
                }
            };
            MenuController.shopOpen[2] += ChangeCostBallText;
            MenuController.shopOpen[2] += () =>
            {
                _countBallsText.text = $"({progress.balls[FieldManager.currentField]+1}/{progress.countCompleteChallenge[FieldManager.currentField]+1})";
                if (IsStartChallenge[FieldManager.currentField])
                {
                    if (!_startChallengeImage.raycastTarget) return;
                    _startChallengeImage.raycastTarget = false;
                    _startChallengeImage.sprite = GameManager.instance._lockedSprite;
                    _startChallengeText.text = "In progress";
                    GameManager.TextDown(_startChallengeText.gameObject);
                }
                else
                {
                    if (_startChallengeImage.raycastTarget) return;
                    _startChallengeImage.raycastTarget = true;
                    _startChallengeImage.sprite = GameManager.instance._unlockedSprite;
                    _startChallengeText.text = "START CHALLENGE";
                    GameManager.TextUp(_startChallengeText.gameObject);

                    if (progress.countCompleteChallenge[FieldManager.currentField] >= 5)
                    {
                        if (!_startChallengeImage.raycastTarget) return;
                        _startChallengeImage.raycastTarget = false;
                        _startChallengeImage.sprite = GameManager.instance._lockedSprite;
                        _startChallengeText.text = "Maximum :(";
                        GameManager.TextDown(_startChallengeText.gameObject);
                    }
                    else
                    {
                        if (_startChallengeImage.raycastTarget) return;
                        _startChallengeImage.raycastTarget = true;
                        _startChallengeImage.sprite = GameManager.instance._unlockedSprite;
                        _startChallengeText.text = "START CHALLENGE";
                        GameManager.TextUp(_startChallengeText.gameObject);
                    }
                }
            };
        }

        private void Start()
        {
            for (int _field = 0; _field < 9; _field++)
            {
                if (progress.currentProgressChallenge[_field] > 0)
                    StartChallenge(_field);
            }

            ChangeTextAndFill();
        }

        private void ChangeCostBallText()
        {
            if (_isMaximum && progress.balls[FieldManager.currentField] > 5) return;
            if (progress.balls[FieldManager.currentField] > 5)
            {
                _isMaximum = true;
                _costBall.text = "MAX";
                GameManager.TextDown(_costBall.gameObject);
                if (_buyBallsImage.raycastTarget)
                {
                    _buyBallsImage.raycastTarget = false;
                    _buyBallsImage.sprite = GameManager.instance._lockedSprite;
                }
            }
            else
            {
                if (_isMaximum)
                {
                    _isMaximum = false;
                    _costBall.text = "450";
                    GameManager.TextUp(_costBall.gameObject);
                    if (!_buyBallsImage.raycastTarget)
                    {
                        _buyBallsImage.raycastTarget = true;
                        _buyBallsImage.sprite = GameManager.instance._unlockedSprite;
                    }
                }
            }

            if (progress.balls[FieldManager.currentField] >=
                progress.countCompleteChallenge[FieldManager.currentField])
            {
                if (!_buyBallsImage.raycastTarget)
                    return;
                _buyBallsImage.raycastTarget = false;
                _buyBallsImage.sprite = GameManager.instance._lockedSprite;
                GameManager.TextDown(_costBall.gameObject);
            }
            else
            {
                if (_buyBallsImage.raycastTarget)
                    return;
                _buyBallsImage.raycastTarget = true;
                _buyBallsImage.sprite = GameManager.instance._unlockedSprite;
                GameManager.TextUp(_costBall.gameObject);
            }
        }


        public void ChangeTextAndFill()
        {
            _challengeProgressText.text =
                $"{progress.currentProgressChallenge[FieldManager.currentField]}/{1000 * (progress.countCompleteChallenge[FieldManager.currentField] + 1)}";
            _challengeProgress.fillAmount = progress.currentProgressChallenge[FieldManager.currentField] /
                                            (1000.0f * (progress.countCompleteChallenge[FieldManager.currentField] +
                                                        1));
        }

        public void CompleteChallenge(int field)
        {
            IsStartChallenge[field] = false;
            if (field == FieldManager.currentField)
            {
                getReward();
                _challengeCanvas.SetActive(false);
                GameManager.instance.bonusCanvas.SetActive(true);
                GameManager.instance.upperCanvas.SetActive(true);
            }
            else
            {
                _progressFills[field].fillAmount = 1;
                _progressTexts[field].text = "COMPLETE";
            }
        }

        private void getReward()
        {
            _getRewardText.text =
                $"You get: {300 + 100 * progress.countCompleteChallenge[FieldManager.currentField]} gems.";
            _completePanel.SetActive(true);
            CloseChallengePanel(FieldManager.currentField);
            PlayerDataController.Gems += 300 + 100 * progress.countCompleteChallenge[FieldManager.currentField];
            if (progress.countCompleteChallenge[FieldManager.currentField] == 1 || progress.countCompleteChallenge[FieldManager.currentField] == 4)
            {
                foreach (var _t in GameManager.instance.fields[FieldManager.currentField].circles)
                {
                    var _localScale = _t.transform.localScale;
                    _localScale = new Vector3(
                        _localScale.x * 2f,
                        _localScale.y * 2f, 1);
                    _t.transform.localScale = _localScale;
                }
            }
            progress.countCompleteChallenge[FieldManager.currentField]++;
            progress.currentProgressChallenge[FieldManager.currentField] = 0;
            
        }

        private void StartChallenge(int field)
        {
            if (field == FieldManager.currentField)
            {
                _challengeCanvas.SetActive(true);

                _newChallengePanel.SetActive(false);
                GameManager.instance.bonusCanvas.SetActive(false);
                GameManager.instance.upperCanvas.SetActive(false);
                ChangeTextAndFill();
            }

            IsStartChallenge[field] = true;
            if (progress.countCompleteChallenge[field] == 1 || progress.countCompleteChallenge[field] == 4)
            {
                foreach (var _t in GameManager.instance.fields[field].circles)
                {
                    var _localScale = _t.transform.localScale;
                    _localScale = new Vector3(
                        _localScale.x / 2f,
                        _localScale.y / 2f, 1);
                    _t.transform.localScale = _localScale;
                }
            }

            OpenChallengePanel(field);
        }

        public void StartNewChallenge()
        {
            _challengeCountText.text = $"({progress.countCompleteChallenge[FieldManager.currentField] + 1}/5)";
            _challengeText.text =
                $"Score {(progress.countCompleteChallenge[FieldManager.currentField] + 1) * 1000} point" +
                _challenges[progress.countCompleteChallenge[FieldManager.currentField]];
            StartCoroutine(startChallenge());
        }

        private IEnumerator startChallenge()
        {
            for (int _i = 0; _i < 70; _i++)
            {
                MenuController.instance._shops[2].transform.position -= new Vector3(0, 0.07f, 0);
                yield return null;
            }

            MenuController.instance.OpenShop(2);
            _newChallengePanel.SetActive(true);
        }

        public void ConfirmStartChallenge()
        {
            StartChallenge(FieldManager.currentField);
        }

        private void OpenChallengePanel(int field)
        {
            _challengePanel[field].SetActive(true);
        }

        public void BuyBall()
        {
            if (PlayerDataController.Gems < 450) return;

            progress.balls[FieldManager.currentField]++;
            PlayerDataController.Gems -= 450;
            ChangeCostBallText();
            _countBallsText.text = $"({progress.balls[FieldManager.currentField]}/6)";
        }

        private void CloseChallengePanel(int field)
        {
            _challengePanel[field].SetActive(false);
        }


        public void OpenChallenges()
        {
            _challengeButton.transform.parent.gameObject.GetComponent<Button>().enabled = true;
            _challengeButton.sprite = _openChallenge;
        }

        private void CloseChallenges()
        {
            _challengeButton.transform.parent.gameObject.GetComponent<Button>().enabled = false;
            _challengeButton.sprite = GameManager.instance._lockFunctionSprite;
        }
    }


    public class ChallengeProgress
    {
        public int[] countCompleteChallenge = {0, 0, 0, 0, 0, 0, 0, 0, 0};
        public int[] currentProgressChallenge = {0, 0, 0, 0, 0, 0, 0, 0, 0};
        public int[] balls = {0, 0, 0, 0, 0, 0, 0, 0, 0};
    }
}