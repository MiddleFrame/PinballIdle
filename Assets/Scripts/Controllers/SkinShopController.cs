using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Controllers
{
    public class SkinShopController : MonoBehaviour
    {

        public static SkinShopController instance;
        
        public static int buyElementX2 = 1;
        public static Skins skins;
        public static int CurrentTrail;
        public static int CurrentAnim;
        private static bool isSpecial = true;

        [SerializeField]
        private Text _specialCost;

        [SerializeField]
        private Text _doubleSpecialCost;

        [SerializeField]
        private Text _animCost;

        [SerializeField]
        private Text _trailCost;

        private static int currentTrail
        {
            get => PlayerPrefs.GetInt("CurrentTrail", 0);
            set => PlayerPrefs.SetInt("CurrentTrail", value);
        }

        private static int currentAnim
        {
            get => PlayerPrefs.GetInt("CurrentAnim", 0);
            set => PlayerPrefs.SetInt("CurrentAnim", value);
        }

        [SerializeField]
        private Text _coinText;

        [SerializeField]
        private Text _timerText;

        [SerializeField]
        private Text _diamondText;

        [SerializeField]
        private GameObject _specialOffer;

        private static GameObject _specialOfferStat;
        private static GameObject _specialTrail;
        private static GameObject _specialAnim;

        [SerializeField]
        private GameObject[] _strokesTrail;

        [SerializeField]
        private GameObject[] _buyButtonTrail;

        [SerializeField]
        private GameObject[] _selectedTextTrail;

        [SerializeField]
        private GameObject[] _strokesAnim;

        [SerializeField]
        private GameObject[] _buyButtonAnim;

        [SerializeField]
        private GameObject[] _selectedTextAnim;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey("SpecialOfferTime"))
            {
                PlayerPrefs.SetString("SpecialOfferTime", DateTime.Now.ToString());
            }
            
            CurrentAnim = currentAnim;
            CurrentTrail = currentTrail;
            for (int _i = 0; _i < skins.trail.Length; _i++)
            {
                if (skins.trail[_i])
                {
                    _buyButtonTrail[_i].SetActive(false);
                }
            }

            for (int _i = 0; _i < skins.anim.Length; _i++)
            {
                if (skins.anim[_i])
                {
                    _buyButtonAnim[_i].SetActive(false);
                }
            }

            

            _strokesAnim[CurrentAnim].SetActive(true);
            _selectedTextAnim[CurrentAnim].SetActive(true);
            _strokesTrail[CurrentTrail].SetActive(true);
            _selectedTextTrail[CurrentTrail].SetActive(true);
            MenuController.openMenu[MenuController.Shops.Shop] += () =>
            {
                _coinText.text = GameManager.NormalSum(PlayerDataController.PointSum);
                _diamondText.text = GameManager.NormalSum(PlayerDataController.Gems);

                if (!IaPurchase.IsIapInitialized()) return;
                if (DateTime.Now >= DateTime.Parse(PlayerPrefs.GetString("SpecialOfferTime",DateTime.Now.ToString())).AddDays(3))
                {
                    _specialCost.text = IaPurchase._storeController.products
                        .WithStoreSpecificID(IaPurchase.NOT_SPECIAL_OFFER)
                        .metadata.localizedPriceString;
                    _doubleSpecialCost.gameObject.SetActive(false);
                    Debug.Log("Not Special");
                    isSpecial = false;
                    _timerText.gameObject.SetActive(false);
                }
                else
                {
                    _specialCost.text = IaPurchase._storeController.products
                        .WithStoreSpecificID(IaPurchase.SPECIAL_OFFER)
                        .metadata.localizedPriceString;
                    _doubleSpecialCost.text = IaPurchase._storeController.products
                        .WithStoreSpecificID(IaPurchase.NOT_SPECIAL_OFFER).metadata
                        .localizedPriceString;
                }
            };
            if (DateTime.Now < DateTime.Parse(PlayerPrefs.GetString("SpecialOfferTime",DateTime.Now.ToString())).AddDays(3))
                StartCoroutine(timer());
            _specialTrail = _buyButtonTrail[7];
            _specialAnim = _buyButtonAnim[3];
            _specialOfferStat = _specialOffer;
            for (int field = 0; field < FieldsFactory.FieldsCount; field++)
            {
                FieldsFactory.GetField(field).spawnTeleport.ChangeTrail(CurrentTrail);
            }
        }


        private IEnumerator timer()
        {
            var _time = DateTime.Parse(PlayerPrefs.GetString("SpecialOfferTime")).AddDays(3) - DateTime.Now;
            Debug.Log("time special offer " + _time);
            while (true)
            {
                _timerText.text = string.Format("{0:%d}D {0:%h}H {0:%m}M {0:%s}S", _time);
                _time = _time.Subtract(new TimeSpan(0, 0, 1));
                if (_time.TotalSeconds <= 0)
                {
                    Debug.Log("Not Special");
                    isSpecial = false;
                    _timerText.gameObject.SetActive(false);
                    yield break;
                }

                yield return new WaitForSeconds(1f);
            }
        }

        public IEnumerator InitSkins()
        {
            while (!IaPurchase.IsIapInitialized())
            {
                yield return new WaitForSeconds(1f);
            }
            if (IaPurchase.CheckProduct("ball_anim_1")||IaPurchase.CheckProduct("ball_anim_1"))
            {
                skins.anim[3] = true;
            }

            if (IaPurchase.CheckProduct("ball_trail") || IaPurchase.CheckProduct("ball_trail_1"))
            {
                skins.trail[7] = true;
            }

            if (skins.trail[7] && skins.anim[3])
            {
                _specialOffer.SetActive(false);
            }
            _animCost.text = IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.BALL_ANIM)
                .metadata.localizedPriceString;
            _trailCost.text = (IaPurchase._storeController.products.WithStoreSpecificID(IaPurchase.BALL_TRAIL)
                .metadata.localizedPriceString);
        }

        public void BuyTrail(int trail)
        {
            if (skins.trail[trail])
            {
                SelectTrail(trail);
                return;
            }

            switch (trail)
            {
                case 0:
                    break;
                case 1:
                    if (PlayerDataController.PointSum < 3000000) return;
                    PlayerDataController.PointSum -= 3000000;
                    break;
                case 2:
                    if (PlayerDataController.PointSum < 20000000) return;
                    PlayerDataController.PointSum -= 20000000;
                    break;
                case 3:
                    if (PlayerDataController.PointSum < 50000000) return;
                    PlayerDataController.PointSum -= 50000000;
                    break;
                case 4:
                    if (PlayerDataController.Gems < 500) return;
                    PlayerDataController.Gems -= 500;
                    break;
                case 5:
                    if (PlayerDataController.Gems < 700) return;
                    PlayerDataController.Gems -= 700;
                    break;
                case 6:
                    if (PlayerDataController.Gems < 900) return;
                    PlayerDataController.Gems -= 900;
                    break;
                case 7:
                    IaPurchase.BuyProductID(IaPurchase.BALL_TRAIL);
                    return;
            }

            skins.trail[trail] = true;
            _buyButtonTrail[trail].SetActive(false);
        }

        private void SelectTrail(int trail)
        {
            _strokesTrail[CurrentTrail].SetActive(false);
            _selectedTextTrail[CurrentTrail].SetActive(false);
            currentTrail = trail;
            CurrentTrail = trail;
            _strokesTrail[trail].SetActive(true);
            _selectedTextTrail[trail].SetActive(true);
            for (int field = 0; field < FieldsFactory.FieldsCount; field++)
            {
                FieldsFactory.GetField(field).spawnTeleport.ChangeTrail(CurrentTrail);
            }
        }

        public void BuyAnim(int anim)
        {
            if (skins.anim[anim])
            {
                SelectAnim(anim);
                return;
            }

            switch (anim)
            {
                case 0:
                    break;
                case 1:
                    if (PlayerDataController.PointSum < 20000000) return;
                    PlayerDataController.PointSum -= 20000000;
                    break;
                case 2:
                    if (PlayerDataController.Gems < 600) return;
                    PlayerDataController.Gems -= 600;
                    break;
                case 3:
                    IaPurchase.BuyProductID(IaPurchase.BALL_ANIM);
                    return;
            }

            skins.anim[anim] = true;
            _buyButtonAnim[anim].SetActive(false);
        }

        public static void ConfBuyTrail()
        {
            skins.trail[7] = true;
            _specialTrail.SetActive(false);
            if (skins.anim[3])
            {
                _specialOfferStat.SetActive(false);
            }
        }

        public static void ConfBuyAnim()
        {
            skins.anim[3] = true;
            _specialAnim.SetActive(false);
            if (skins.trail[7])
            {
                _specialOfferStat.SetActive(false);
            }
        }

        private void SelectAnim(int anim)
        {
            _strokesAnim[CurrentAnim].SetActive(false);
            _selectedTextAnim[CurrentAnim].SetActive(false);
            currentAnim = anim;
            CurrentAnim = anim;
            _strokesAnim[anim].SetActive(true);
            _selectedTextAnim[anim].SetActive(true);
        }

        public void BuySpecialOffer()
        {
            IaPurchase.BuyProductID(isSpecial ? IaPurchase.SPECIAL_OFFER : IaPurchase.NOT_SPECIAL_OFFER);
        }
    }
}


public class Skins
{
    public bool[] trail = {true, false, false, false, false, false, false, false};
    public bool[] anim = {true, false, false, false};
}