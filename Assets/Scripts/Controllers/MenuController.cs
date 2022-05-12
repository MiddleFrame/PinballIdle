using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private const int ALL_FIELD = -2;
    public static int currentMenu = -1;

    public static MenuController instance;
    
    public GameObject[] _shops;

    [SerializeField]
    private GameObject[] _buttons;

    [SerializeField]
    private GameObject _backPanel;

    [SerializeField]
    private GameObject _stats;

    public static Action[] shopOpen = new Action[6];

    private void Awake()
    {
        instance = this;
    }

    public void OpenShop(int shop)
    {
        currentMenu = -1;
        for (int _j = 0; _j < _shops.Length; _j++)
        {
            if (shop != _j && _shops[_j].activeSelf)
            {
                OpenShop(_j);
            }
        }

        if (shop == ALL_FIELD)
        {
            return;
        }

        if (!_shops[shop].activeSelf)
        {
            shopOpen[shop]?.Invoke();
            currentMenu = shop;
            _buttons[shop].GetComponent<Image>().color = ThemeManager.instance.themes[ThemeManager.currentTheme].lightGray;
            _backPanel.SetActive(true);
        }
        else
        {
            if (shop == _shops.Length - 1)
                _stats.SetActive(false);
            _buttons[shop].GetComponent<Image>().color =
                ThemeManager.instance.themes[ThemeManager.currentTheme].fieldColor;
            _backPanel.SetActive(false);
        }

        _shops[shop].SetActive(!_shops[shop].activeSelf);
    }

    public void BackPanelClick()
    {
        for (int _j = 0; _j < _shops.Length; _j++)
        {
            if (_shops[_j].activeSelf)
            {
                OpenShop(_j);
            }
        }
    }
}