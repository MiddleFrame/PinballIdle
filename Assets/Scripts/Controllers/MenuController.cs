using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        private const int ALL_FIELD = 0;
        public static int currentMenu = -1;
        public enum Shops
        {
            AllField=0,
            Shop=1,
            Versus=2,
            Ranks=3,
            UpgradeFields=4,
            Quests=5
        }

        public static MenuController instance;

        public GameObject[] _shops;
        public GameObject[] _back;



        public static Dictionary<Shops, Action> openMenu = new Dictionary<Shops, Action>()
        {
            {Shops.AllField, null},
            {Shops.Quests, null},
            {Shops.Ranks, null},
            {Shops.Shop, null},
            {Shops.Versus, null},
            {Shops.UpgradeFields, null}
        };


        private void Awake()
        {
            instance = this;
            FieldManager.openOneField += BackPanelClick;
        }

        public void OpenShop(int numShop)
        {
            Shops _shop = (Shops) numShop;
            if (currentMenu == (int) _shop && currentMenu != ALL_FIELD)
            {
                currentMenu = -1;
                _shops[(int) _shop-1].SetActive(false);
                if (numShop < 3)
                {
                    _back[numShop].SetActive(false);
                }
                return;
            }
            if (numShop < 3)
            {
                _back[numShop].SetActive(true);
            }

            if ((int) _shop == ALL_FIELD)
            {
                FieldManager.instance.OpenAllFields();
                return;
            }

            if (currentMenu != -1 && currentMenu != ALL_FIELD)
            {
                OpenShop(currentMenu);
            }
            
            openMenu[_shop]?.Invoke();
            currentMenu = (int) _shop;
            _shops[(int) _shop-1].SetActive(true);
        }

        public void BackPanelClick()
        {
            for (int _j = 0; _j < _shops.Length; _j++)
            {
                if (_shops[_j].activeSelf)
                {
                    OpenShop(_j+1);
                }
            }
        }
    }
}