using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Competition
{
    public class ButtonsBuff : MonoBehaviour
    {
        [SerializeField]
        private int cost;
        [SerializeField]
        private Sprite open;
        [SerializeField]
        private Sprite close;

        private bool _isOpen = true;

        private  Button _button;

        private  Image _image;

        [SerializeField]
        private int _type;
        private  GameObject _imageChild;
        private  Text _textChild;
        // Update is called once per frame

        private void Start()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _imageChild = transform.GetChild(0).gameObject;
            _textChild = GetComponentInChildren<Text>();
            _button.onClick.AddListener(UpdateCost); 
            _textChild.text = cost.ToString();
            
        }
        
        
        private void Update()
        {
            if (_type ==1 && CompetitionManager.isUpgradeBuff[0] && !_isOpen) return;
            if (_type ==1 && CompetitionManager.isUpgradeBuff[0])
            {
                _isOpen = false;
                _image.sprite = close;
                CompetitionManager.ElementDown(_imageChild);
                _image.raycastTarget = false;
                return;
            }
            if (_isOpen && cost > CompetitionManager.PlayerPoint)
            {
                _isOpen = false;
                _image.sprite = close;
                CompetitionManager.ElementDown(_imageChild);
                _image.raycastTarget = false;
            }
            else if (!_isOpen && cost <= CompetitionManager.PlayerPoint)
            {
                _isOpen = true;
                _image.sprite = open;
                CompetitionManager.ElementUp(_imageChild);
                _image.raycastTarget = true;
            }
        }

        private void UpdateCost()
        {
            switch (_type)
            {
                case 0:
                    CompetitionManager.BuyUpgrade(0,cost);
                    break;
                case 1 :
                    CompetitionManager.BuyUpgradeBuff(0,cost);
                    break;
                case 2:
                    CompetitionManager.BuyBall(0,cost);
                    break;
                case 3:
                    CompetitionManager.BuyTrap(0,cost);
                    break;
            }
            switch (_type)
            {
                case 0:
                    cost += 50;
                    break;
                case 1:
                    cost += 100;
                    break;
                case 2:
                    cost += 200;
                    break;
                case 3:
                    break;
                default:
                    cost = (int)(cost*1.5f);
                    break;
                
            }
            _textChild.text = cost.ToString();
        }
    }
}
