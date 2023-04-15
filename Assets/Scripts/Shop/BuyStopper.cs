using Managers;
using UnityEngine;

namespace Shop
{
    public class BuyStopper : MonoBehaviour
    {
        public static StopperGrades grades;

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
                    OpenStoppers(_i);
            }
        }

        public void OpenStoppers(int i)
        {
            FieldsFactory.GetField(i).OpenStopper();
        }
        public void CloseStoppers(int i)
        {
            FieldsFactory.GetField(i).CloseStopper();
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