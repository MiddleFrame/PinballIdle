using Controllers;
using Managers;
using UnityEngine;
using Zenject;

namespace Shop
{
    public class StopperUnlocked : MonoBehaviour
    {
        private const int COST_TO_BUY_STOPPER = 50000;

        [SerializeField]
        private GameObject _buyStopper;
        [Inject]
        private PopupsController _popupsController;
        private void Start()
        {
            _popupsController.RegisterActionToOpenPopup<FieldLevelPanel>(OpenUpgradeField);
     
        }

        public void BuyStopper()
        {
            if (PlayerDataController.PointSum < COST_TO_BUY_STOPPER) return;
            PlayerDataController.PointSum -= COST_TO_BUY_STOPPER;
            DefaultBuff.grade.stopper[FieldManager.currentField] = true;
            _buyStopper.SetActive(false);
        }

        public void OpenStopper()
        {
            _buyStopper.SetActive(true);
        }

        private void OpenUpgradeField()
        {
            _buyStopper.SetActive(QuestManager.progress[FieldManager.currentField + 1].isComplete[2]);
            if (DefaultBuff.grade.stopper[FieldManager.currentField])
                _buyStopper.SetActive(false);
        }
    }
}