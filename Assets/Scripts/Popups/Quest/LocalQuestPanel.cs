using Controllers;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Quest
{
    public class LocalQuestPanel : MonoBehaviour
    {
        [SerializeField]
        private Image[] _fillsFieldQuest;


        private QuestManager _questManager;
        [SerializeField]
        private Text[] _textsFieldQuest;
        internal void Init(QuestManager questManager)
        {
            _questManager = questManager;
        }

        public void UpdateVisualQuest()
        {
            for (int _index = 0; _index < _questManager.needToLocalQuest.Length; _index++)
            {
                _textsFieldQuest[_index].text =
                    (PlayerDataController.playerStats.lvl[FieldManager.currentField] - 1 >= _questManager.needToLocalQuest[_index]
                        ? _questManager.needToLocalQuest[_index]
                        : PlayerDataController.playerStats.lvl[FieldManager.currentField] - 1) + $"/{_questManager.needToLocalQuest[_index]}";
                _fillsFieldQuest[_index].fillAmount =
                    (float) (PlayerDataController.playerStats.lvl[FieldManager.currentField] - 1) / _questManager.needToLocalQuest[_index];
            }
        }
    }
}