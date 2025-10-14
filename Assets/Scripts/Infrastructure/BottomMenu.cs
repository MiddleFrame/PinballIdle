using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace
{
    public class BottomMenu:MonoBehaviour
    {
        [SerializeField]
        private Button _backShop, _openShop;
        [SerializeField]
        private Button _backAllField, _openAllField;
        [SerializeField]
        private Button _backVS, _openCompetitive;
        
        [Inject]
        private PopupsController _popupsController;

        private Popup _shopPopup, _competitivePopup, _allFieldPopup;
        private void Start()
        {
            RegisterButtons();
        }

        private void RegisterButtons()
        {
            _openAllField.onClick.AddListener(OpenAllFields);
            _backAllField.onClick.AddListener(BackAllField);
            _openShop.onClick.AddListener(OpenShop);
            _backShop.onClick.AddListener(BackShop);
            _openCompetitive.onClick.AddListener(OpenCompetitive);
            _backVS.onClick.AddListener(BackCompetitive);
        }
        private void OpenShop()
        {
            _shopPopup = _popupsController.ShowPopup<ShopPanel>();
            _backShop.gameObject.SetActive(true);
        }

        private void OpenCompetitive()
        {
            _competitivePopup = _popupsController.ShowPopup<CompetitivePanel>();
            _backVS.gameObject.SetActive(true);
        }

        private void OpenAllFields()
        {
            _allFieldPopup = _popupsController.ShowPopup<AllFieldPanel>();
            _backAllField.gameObject.SetActive(true);
        }

        private void BackShop()
        {
            _backShop.gameObject.SetActive(false);
            _shopPopup.Close();
        }
        private void BackAllField()
        {
            _backAllField.gameObject.SetActive(false);
            _allFieldPopup.Close();
            FindFirstObjectByType<FieldManager>().OpenLastField();
        }
        private void BackCompetitive()
        {
            _backVS.gameObject.SetActive(false);
            _competitivePopup.Close();
        }
    }
}