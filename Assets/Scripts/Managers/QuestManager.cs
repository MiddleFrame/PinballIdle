using Controllers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class QuestManager : MonoBehaviour
    {
        public static Quest[] progress;

        [SerializeField]
        private Text _title;

        [SerializeField]
        private GameObject FieldQuest;

        [SerializeField]
        private GameObject FieldQuestStroke;

        [SerializeField]
        private GameObject GlobalQuestStroke;

        [SerializeField]
        private Image[] _fillsFieldQuest;

        [SerializeField]
        private Image[] _fillsGlobalQuest;

        [SerializeField]
        private Image[] _buttonsGlobalQuest;

        [SerializeField]
        private Text[] _textsFieldQuest;

        [SerializeField]
        private Text[] _textsGlobalQuest;

        [SerializeField]
        private GameObject GlobalQuest;
        [SerializeField]
        private GameObject _notification;

        public static QuestManager instance;

        public UnityEvent[] completeGlobalEvent;

        public UnityEvent<int>[] completeLocalEvent;

        private readonly int[] _needToLocalQuest = new[] {1, 3, 6};
        private readonly int[] _needToGlobalQuest = new[] {50, 100, 200, 5, 10, 20, 10, 20, 30, 40, 50, 60, 70, 80, 90};

        [SerializeField]
        private GameObject[] _iconQuest;

        private void Awake()
        {
            FieldManager.openOneField += () => { CheckLevelQuest(FieldManager.currentField); };
            OpenFieldQuest();
            instance = this;
        }

        public void OpenGlobalQuest()
        {
            _title.text = "Global Quest";
            if (!GlobalQuest.activeSelf)
                GlobalQuest.SetActive(true);
            if (FieldQuest.activeSelf)
                FieldQuest.SetActive(false);
            if (FieldQuestStroke.activeSelf)
                FieldQuestStroke.SetActive(false);
            if (!GlobalQuestStroke.activeSelf)
                GlobalQuestStroke.SetActive(true);

            UpdateGlobalQuest();
        }

        public void OpenFieldQuest()
        {
            _title.text = "Field Quest";
            if (GlobalQuest.activeSelf)
                GlobalQuest.SetActive(false);
            if (!FieldQuest.activeSelf)
                FieldQuest.SetActive(true);

            if (!FieldQuestStroke.activeSelf)
                FieldQuestStroke.SetActive(true);
            if (GlobalQuestStroke.activeSelf)
                GlobalQuestStroke.SetActive(false);
        }

        public void InitializeQuest()
        {
            for (int _i = 0; _i < GameManager.instance.fields.Length; _i++)
            {
                CheckLevelQuest(_i);
                EventLevelQuest(_i);
            }

            InitGlobalQuest();
            UpdateGlobalQuest();
        }

        public void OpenTriple(int field)
        {
            GameManager.instance.fields[field].MakeTriple();
        }

        // public void CompleteLevelQuest(int field)
        // {
        //     for (var _index = 0; _index < completeLocalEvent.Length; _index++)
        //     {
        //         if (progress[field + 1].isComplete[_index] ||
        //             PlayerDataController.playerStats.lvl[field]-1 != _needToLocalQuest[_index]) continue;
        //         progress[field + 1].isComplete[_index] = true;
        //         completeLocalEvent[_index].Invoke(field);
        //         if (field == FieldManager.currentField)
        //             _iconQuest[_index].SetActive(false);
        //     }
        // }

        public void CheckLevelQuest(int field)
        {
            for (var _index = 0; _index < completeLocalEvent.Length; _index++)
            {
                if (FieldManager.currentField == field)
                {
                    if (progress[field + 1].isComplete[_index] && _iconQuest[_index].activeSelf)
                        _iconQuest[_index].SetActive(false);
                    else if (!progress[field + 1].isComplete[_index] && !_iconQuest[_index].activeSelf)
                    {
                        _iconQuest[_index].SetActive(true);
                    }

                    _textsFieldQuest[_index].text =
                        (PlayerDataController.playerStats.lvl[field] - 1 >= _needToLocalQuest[_index]
                            ? _needToLocalQuest[_index]
                            : PlayerDataController.playerStats.lvl[field] - 1) + $"/{_needToLocalQuest[_index]}";
                    _fillsFieldQuest[_index].fillAmount =
                        (float) (PlayerDataController.playerStats.lvl[field] - 1) / _needToLocalQuest[_index];
                }

                if (progress[field + 1].isComplete[_index] ||
                    PlayerDataController.playerStats.lvl[field] - 1 < _needToLocalQuest[_index]) continue;
                progress[field + 1].isComplete[_index] = true;
                completeLocalEvent[_index].Invoke(field);
                if (FieldManager.currentField == field)
                {
                    _iconQuest[_index].SetActive(false);
                    if (_index == 1 && TutorialManager._isNeedTutorialRank)
                        TutorialManager.RankTutorialWindow();
                }

                AnalyticManager.CompleteLocalQuest(field, _index);
            }
        }

        public void UpdateGlobalQuest()
        {
            for (var _index = 0; _index < completeGlobalEvent.Length; _index++)
            {
                if (progress[0].isComplete[_index]) continue;
                _textsGlobalQuest[_index].text = (
                    progress[0].progressQuest[_index] >= _needToGlobalQuest[_index]
                        ? _needToGlobalQuest[_index]
                        : progress[0].progressQuest[_index]) + $"/{_needToGlobalQuest[_index]}";
                _fillsGlobalQuest[_index].fillAmount =
                    (float) progress[0].progressQuest[_index] / _needToGlobalQuest[_index];
                if (progress[0].progressQuest[_index] >= _needToGlobalQuest[_index])
                {
                    _buttonsGlobalQuest[_index].raycastTarget = true;
                    _buttonsGlobalQuest[_index].sprite = GameManager.instance._unlockedSprite;
                    if(!_notification.activeSelf)
                        _notification.SetActive(true);
                }
            }
        }

        public void GiveGems(int gems)
        {
            PlayerDataController.Gems += gems;
        }

        public void GiveKeys(int keys)
        {
            PlayerDataController.Key += keys;
        }

        public void GiveReward(int quest)
        {
            progress[0].isComplete[quest] = true;
            completeGlobalEvent[quest].Invoke();
            _buttonsGlobalQuest[quest].gameObject.SetActive(false);
            
            _notification.SetActive(false);
        }

        private void InitGlobalQuest()
        {
            for (var _index = 0; _index < completeGlobalEvent.Length; _index++)
            {
                if (progress[0].isComplete[_index])
                {
                    _textsGlobalQuest[_index].text = $"{_needToGlobalQuest[_index]}/{_needToGlobalQuest[_index]}";
                    _fillsGlobalQuest[_index].fillAmount = 1;
                    _buttonsGlobalQuest[_index].gameObject.SetActive(false);
                }
            }
        }

        private void EventLevelQuest(int field)
        {
            for (var _index = 0; _index < completeLocalEvent.Length; _index++)
            {
                if (!progress[field + 1].isComplete[_index] &&
                    progress[field + 1].progressQuest[_index] >= _needToLocalQuest[_index])
                    progress[field + 1].isComplete[_index] = true;
                if (progress[field + 1].isComplete[_index])
                    completeLocalEvent[_index].Invoke(field);
            }
        }
    }


    public class Quest
    {
        public bool[] isComplete;
        public int[] progressQuest;

        public Quest(int count)
        {
            isComplete = new bool[count];
            progressQuest = new int[count];
        }
    }
}