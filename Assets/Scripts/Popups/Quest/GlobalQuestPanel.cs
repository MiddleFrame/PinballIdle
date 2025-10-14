using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Popups.Quest
{
    public class GlobalQuestPanel:MonoBehaviour
    {
        [SerializeField]
        private GameObject _stroke;

        [SerializeField]
        private Image[] _fillsGlobalQuest;

        [SerializeField]
        private Button[] _buttonsGlobalQuest;

        [SerializeField]
        private Text[] _textsGlobalQuest;

        private QuestManager _questManager;
        
        public void Open()
        {
            gameObject.SetActive(true);
            _stroke.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
            _stroke.SetActive(false);
        }
        
        internal void Init(QuestManager questManager)
        {
            _questManager = questManager;
            InitButton();
        }

        private void InitButton()
        {
            for (var _index = 0; _index < _buttonsGlobalQuest.Length; _index++)
            {
                var _index1 = _index;
                _buttonsGlobalQuest[_index].onClick.AddListener(()=>_questManager.GiveReward(_index1, _buttonsGlobalQuest[_index1].gameObject));
            }
        }
        
        public void UpdateVisualQuest()
        {
            for (var _index = 0; _index < _textsGlobalQuest.Length; _index++)
            {
                if (QuestManager.progress[0].isComplete[_index])
                    if (QuestManager.progress[0].isComplete[_index])
                    {
                        _textsGlobalQuest[_index].text = $"{_questManager.needToGlobalQuest[_index]}/{_questManager.needToGlobalQuest[_index]}";
                        _fillsGlobalQuest[_index].fillAmount = 1;
                        _buttonsGlobalQuest[_index].gameObject.SetActive(false);
                        continue;
                    }
                
                _textsGlobalQuest[_index].text = (
                    QuestManager.progress[0].progressQuest[_index] >= _questManager.needToGlobalQuest[_index]
                        ? _questManager.needToGlobalQuest[_index]
                        : QuestManager.progress[0].progressQuest[_index]) + $"/{_questManager.needToGlobalQuest[_index]}";
                _fillsGlobalQuest[_index].fillAmount =
                    (float) QuestManager.progress[0].progressQuest[_index] / _questManager.needToGlobalQuest[_index];
                if (QuestManager.progress[0].progressQuest[_index] >= _questManager.needToGlobalQuest[_index])
                {
                    _buttonsGlobalQuest[_index].image.raycastTarget = true;
                    _buttonsGlobalQuest[_index].image.sprite = GameManager.instance._unlockedSprite;
                    
                }
                
            }
        }
    }
}