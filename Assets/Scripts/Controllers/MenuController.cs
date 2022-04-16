using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static int currentMenu = -1;
    [SerializeField]
    private GameObject[] _shops;
    [SerializeField]
    private GameObject[] _buttons;
    [SerializeField]
    private GameObject _backPanel;
    [SerializeField]
    private GameObject _stats;
    public void OpenShop(int i)
    {
        currentMenu = -1;
        for (int j = 0; j < _shops.Length; j++)
        {
            if (i != j && _shops[j].activeSelf)
            {
                OpenShop(j);
            }
        }

        if (!_shops[i].activeSelf)
        {
            currentMenu = i;
            //TODO
            _buttons[i].GetComponent<Image>().color = Color.white;// mycolor;
            _backPanel.SetActive(true);
        }
        else
        {
            if (i == _shops.Length - 1)
                _stats.SetActive(false);
            _buttons[i].GetComponent<Image>().color = new Color(0xE6, 0xE6, 0xE6, 0xFF); ;
            _backPanel.SetActive(false);
        }

        _shops[i].SetActive(!_shops[i].activeSelf);

    }

    public void BackPanelClick()
    {
        for (int j = 0; j < _shops.Length; j++)
        {
            if (_shops[j].activeSelf)
            {
                OpenShop(j);
            }
        }
    }
}
