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
        internal GameObject _notification;

        public static QuestManager instance;

        public UnityEvent[] completeGlobalEvent;

        public UnityEvent<int>[] completeLocalEvent;

        internal readonly int[] needToLocalQuest = {5, 7, 9};

        internal readonly int[] needToGlobalQuest = {50, 100, 200, 5, 10, 20, 10, 20, 30, 40, 50, 60, 70, 80, 90};

        private void Awake()
        {
            FieldManager.openOneField += () => { CheckLocalQuest(FieldManager.currentField); };
            instance = this;
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

        private void EnableNotification()
        {
            _notification.SetActive(true);
        }

        private void DisableNotification()
        {
            _notification.SetActive(false);
        }

        public void CheckLocalQuest(int field)
        {
            for (var _index = 0; _index < completeLocalEvent.Length; _index++)
            {
                if (PlayerDataController.playerStats.lvl[field] - 1 < needToLocalQuest[_index]) continue;
                EnableNotification();
                progress[field + 1].isComplete[_index] = true;
                if (FieldManager.currentField == field)
                {
                    if (_index == 1 && TutorialManager._isNeedTutorialRank)
                        TutorialManager.RankTutorialWindow();
                }

                AnalyticManager.CompleteLocalQuest(field, _index);
            }
        }

        public void CheckGlobalQuest(int quest)
        {
            if (progress[0].isComplete[quest])
                return;
            
            if (progress[0].progressQuest[quest] >= needToGlobalQuest[quest])
                EnableNotification();
        }


        public void GiveGems(int gems)
        {
            PlayerDataController.Gems += gems;
        }


        public void GiveReward(int quest, GameObject button)
        {
            progress[0].isComplete[quest] = true;
            completeGlobalEvent[quest].Invoke();
            button.gameObject.SetActive(false);

            DisableNotification();
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