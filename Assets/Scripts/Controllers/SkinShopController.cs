using System;
using System.Globalization;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class SkinShopController : MonoBehaviour
    {
        [SerializeField]
        private Text _coinText;

        [SerializeField]
        private Text _diamondText;


        private void Start()
        {
            MenuController.shopOpen[4] += () =>
            {
                _coinText.text = GameManager.NormalSum(PlayerDataController.PointSum);
                _diamondText.text = GameManager.NormalSum(PlayerDataController.Gems);
            };
        }
    }
}
