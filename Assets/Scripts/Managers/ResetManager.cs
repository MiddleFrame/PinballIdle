using Controllers;
using Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ResetManager : MonoBehaviour
    {
        [SerializeField]
        private Image _resetButton;

        private void Start()
        {
            MenuController.openMenu[MenuController.Shops.Ranks] += () =>
            {
                if (ChallengeManager.progress.countCompleteChallenge[FieldManager.currentField] >= 5)
                {
                    if (_resetButton.raycastTarget) return;
                    _resetButton.raycastTarget = true;
                    _resetButton.sprite = GameManager.instance._unlockedSprite;
                }
                else
                {
                    if (!_resetButton.raycastTarget) return;
                    _resetButton.raycastTarget = false;
                    _resetButton.sprite = GameManager.instance._lockedSprite;
                }
            };
        }

        public void ResetCurrentField()
        {
            ResetProgressOnField(FieldManager.currentField);
        }

        private void ResetProgressOnField(int field)
        {
            DefaultBuff.ResetProgress(field);
            BuyStopper.grades.isStopper[field] = false;
            BuyStopper.instance.closeStoppers(field);
            PlayerDataController.playerStats.lvl[field] = 1;
            PlayerDataController.Key += 1;
            PlayerDataController.playerStats.exp[field] = 0;
            for (var _index = 0; _index < QuestManager.progress[field + 1].isComplete.Length; _index++)
            {
                QuestManager.progress[field + 1].isComplete[_index] = false;
                QuestManager.progress[field + 1].progressQuest[_index] = 0;
            }

            MenuController.instance.OpenShop(3);
            //Тут мб оставлять N шаров и некоторые открытия
            ChallengeManager.progress.balls[field] = 0; //DefaultBuff.grade.multiplyPoint[field] < 1.7f ? 0 : 1;
            GameManager.instance.spawnPoints[field].ResetBalls();
            ChallengeManager.progress.countCompleteChallenge[field] = 0;
            //  DefaultBuff.grade.multiplyPoint[field] < 1.7f ? 0 : 1;
            ChallengeManager.progress.currentProgressChallenge[field] = 0;
            FieldManager.openOneField.Invoke();
        }
    }
}