using Managers;
using UnityEngine;

namespace Shop
{
    public class BuyStopper : MonoBehaviour
    {
        public static StopperGrades grades;

        [SerializeField]
        private GameObject[] _stoppers;

        public static BuyStopper instance;

        private void Awake()
        {
            instance = this;
            //MenuController.openMenu[MenuController.Shops.UpgradeFields] += changeText;
        }

        private void Start()
        {
            for (int _i = 0; _i < FieldManager.fields.isOpen.Length; _i++)
            {
                if (!FieldManager.fields.isOpen[_i]) continue;
                if (grades.isStopper[_i])
                    openStoppers(_i);
            }
        }

        public void openStoppers(int i)
        {
            _stoppers[i].SetActive(true);
        }
        public void closeStoppers(int i)
        {
            _stoppers[i].SetActive(false);
        }

    }


    public class StopperGrades
    {
        public bool[] isStopper;

        public StopperGrades(int length)
        {
            isStopper = new bool[length];
        }
    }
}