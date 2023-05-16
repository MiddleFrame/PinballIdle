using Controllers;
using Managers;
using UnityEngine;

namespace Shop
{
    public class StopperUnlocked : MonoBehaviour
    {
        private const int COST_TO_BUY_STOPPER = 10000;

        [SerializeField]
        private GameObject _buyStopper;


        private void Start()
        {
            MenuController.openMenu[MenuController.Shops.UpgradeFields] += () =>
            {
                _buyStopper.SetActive(!DefaultBuff.grade.stopper[FieldManager.currentField]);
            };
        }

        public void BuyStopper()
        {
            if (PlayerDataController.PointSum < COST_TO_BUY_STOPPER) return;
            PlayerDataController.PointSum -= COST_TO_BUY_STOPPER;
            DefaultBuff.grade.stopper[FieldManager.currentField] = true;
            _buyStopper.SetActive(false);
        }
    }
}