using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#if UNITY_ADV
using UnityEngine.Advertisements;
using static UnityEngine.Advertisements.Advertisement;
#elif ADMOB
using GoogleMobileAds.Api;
#endif

namespace Managers
{
    public class AdService : MonoBehaviour
#if UNITY_ADV
        , IUnityAdsLoadListener, IUnityAdsInitializationListener, IUnityAdsShowListener
#elif ADMOB
#endif
    {
        private string _gameId;
        private string _androidGameId;

        private const string BANNER_UNIT =
            //"Banner_Android";
            "ca-app-pub-3940256099942544/6300978111";

        private const string BANNER_UNIT_HIGH =
            //"Banner_hight";
            //"ca-app-pub-3940256099942544/6300978111";
            "ca-app-pub-4974660526231227/8284539881";
        private const string INTERSTITIAL_HIGH =
            //"Interstitial_Hight";
            "ca-app-pub-4974660526231227/7159994620";
            //"ca-app-pub-3940256099942544/1033173712";

        private const string INTERSTITIAL_MEDIUM =
            //"Interstitial_Medium";
            "ca-app-pub-3940256099942544/1033173712";

        private const string INTERSTITIAL_LOW =
            //"Interstitial_auto";
            "ca-app-pub-3940256099942544/1033173712";

        private const string REWARD_HIGH =
            "ca-app-pub-4974660526231227/1695388297";
           // "ca-app-pub-3940256099942544/5224354917";
        //"Hight_Rewarded";

        private const string REWARD_MEDIUM =
            //"Medium_Rewarded";
            "ca-app-pub-3940256099942544/5224354917";

        private const string REWARD_LOW =
            //"Rewarded_Android";
            "ca-app-pub-3940256099942544/5224354917";
#if UNITY_ADV
        BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
#elif ADMOB

        private RewardedAd _rewardedAd;
        private InterstitialAd _interstitialAd;
        BannerView _bannerView;
#endif
        private bool rewardHighLoaded;
        private bool rewardMediumLoaded;
        private bool rewardLowLoaded;

        private bool interstitialHighLoaded;
        private bool interstitialMediumLoaded;
        private bool interstitialLowLoaded;

        private static int _time;

        private const string ID = "4265503";

        public static AdService instanse;

        private bool IsInit =>
#if UNITY_ADV
isInitialized;
#elif ADMOB
            _isInitialized;

        private bool _isInitialized = false;
#else
            false;
#endif
        private bool IsSup =>
#if UNITY_ADV
isSupported;
#elif ADMOB
            true;
#else
            false;
#endif

        private void Awake()
        {
            DontDestroyOnLoad(this);
            instanse = this;
        }

        private IEnumerator Start()
        {
            yield return new WaitWhile(()=> SceneManager.GetActiveScene().name=="BootScene");
            if (AdsAndIAP.isRemoveAds)
                yield break;
            _androidGameId = ID;
            InitializeAds();
        }

        private void InitializeAds()
        {
            _gameId = _androidGameId;

            if (!IsInit && IsSup)
            {
#if UNITY_ADV
                Initialize(_gameId, false, this);

#elif ADMOB
                MobileAds.Initialize((InitializationStatus initStatus) =>
                {
                    RequestRewarded();
                    RequestBanner();
                    RequestInterstitial();
                    _isInitialized = true;
                });
#endif
            }
        }

        #region Reward

        private void RequestRewarded()
        {
#if UNITY_ADV
            Load(REWARD_HIGH, this);
            Load(REWARD_MEDIUM, this);
            Load(REWARD_LOW, this);
#elif ADMOB
            LoadRewarded(REWARD_HIGH);
#endif
        }


        public void ShowReward(UnityEvent unityAction)
        {
            _time = 0;
            if (AdsAndIAP.isRemoveAds)
            {
                unityAction.Invoke();
                return;
            }

            if (rewardHighLoaded)
            {
                receive = unityAction;
                Show(REWARD_HIGH, this);
                rewardHighLoaded = false;
                RequestRewarded();
            }
#if UNITY_ADV
            else if (rewardMediumLoaded)
            {
                receive = unityAction;
                Show(REWARD_MEDIUM, this);
                RequestRewarded();
            }
            else if (rewardLowLoaded)
            {
                receive = unityAction;
                Show(REWARD_LOW, this);
                RequestRewarded();
            }
#endif
            else
            {
                GameManager.instance.StartCoroutine(TryToShowRewarded(unityAction));
            }
        }

        private IEnumerator TryToShowRewarded(UnityEvent unityAction)
        {
            for (int _i = 0; _i < 5; _i++)
            {
                yield return new WaitForSeconds(0.3f);
                if (rewardHighLoaded)
                {
                    receive = unityAction;
                    Show(REWARD_HIGH, this);
                    rewardHighLoaded = false;
                    RequestRewarded();
                    yield break;
                }
#if UNITY_ADV
                if (rewardMediumLoaded)
                {
                    receive = unityAction;
                    Show(REWARD_MEDIUM, this);
                    RequestRewarded();
                    rewardMediumLoaded = false;
                }

                if (rewardLowLoaded)
                {
                    receive = unityAction;
                    Show(REWARD_LOW, this);
                    RequestRewarded();
                    rewardLowLoaded = false;
                }
#endif
            }
        }


        private static UnityEvent receive;

        public AdService(string androidGameId)
        {
            _androidGameId = androidGameId;
        }

        #endregion

        #region Banner

        public void ShowBanner()
        {
            if (AdsAndIAP.isRemoveAds) return;
#if UNITY_ADV
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
            Banner.Show(BANNER_UNIT_HIGH, options);
            if (_higheBannerIsShowing) return;

            Banner.Show(BANNER_UNIT, options);
#elif ADMOB
            _bannerView.Show();
#endif
        }

        private void RequestBanner()
        {
#if UNITY_ADV
            Banner.SetPosition(_bannerPosition);
            LoadBanner();
            Banner.Hide();
#elif ADMOB
            if (_bannerView != null)
            {
                DestroyAd();
            }

            // Create a 320x50 banner at top of the screen
            _bannerView = new BannerView(BANNER_UNIT_HIGH, AdSize.Banner, AdPosition.Bottom);
            LoadBanner();
            _bannerView.Hide();
#endif
        }

        public void LoadBanner()
        {
#if UNITY_ADV
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            // Load the Ad Unit with banner content:
            Banner.Load(BANNER_UNIT_HIGH, options);
            Banner.Load(BANNER_UNIT, options);
#elif ADMOB
            if(_bannerView == null)
            {
                RequestBanner();
            }

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            _bannerView.LoadAd(adRequest);
#endif
        }


        void OnBannerClicked()
        {
        }

        void OnBannerShown()
        {
        }

        void OnBannerHidden()
        {
        }

        void OnBannerLoaded()
        {
            Debug.Log("Banner loaded");
        }

        // Implement code to execute when the load errorCallback event triggers:
        void OnBannerError(string message)
        {
            Debug.LogError($"Banner Error: {message}");
            // Optionally execute additional code, such as attempting to load another ad.
        }

        public void HideBanner()
        {
            if (AdsAndIAP.isRemoveAds) return;
#if UNITY_ADV
            Banner.Hide();
#elif ADMOB
#endif
        }

        #endregion

        #region Interstitial

        public void ShowInterstitial()
        {
            _time = 0;
            if (AdsAndIAP.isRemoveAds) return;
            if (interstitialHighLoaded)
            {
#if UNITY_ADV
                Show(INTERSTITIAL_HIGH, this);
#elif ADMOB
                ShowInterstitial(INTERSTITIAL_HIGH);
#endif
                RequestInterstitial();
                interstitialHighLoaded = false;
            }
#if UNITY_ADV
            else if (interstitialMediumLoaded)
            {
                Show(INTERSTITIAL_MEDIUM, this);
                RequestInterstitial();
                interstitialMediumLoaded = false;
            }
            else if (interstitialLowLoaded)
            {
                Show(INTERSTITIAL_LOW, this);
                RequestInterstitial();
                interstitialLowLoaded = false;
            }
#endif
            else
            {
                RepeatedAttemptToShowInterstitial();
            }
        }

        private async void RepeatedAttemptToShowInterstitial()
        {
            for (int _i = 0; _i < 5; _i++)
            {
                await Task.Delay(300);
                if (interstitialHighLoaded)
                {
#if UNITY_ADV
                Show(INTERSTITIAL_HIGH, this);
#elif ADMOB
                    ShowInterstitial(INTERSTITIAL_HIGH);
#endif
                    RequestInterstitial();
                    interstitialHighLoaded = false;
                }
#if UNITY_ADV
                if (interstitialMediumLoaded)
                {
                    Show(INTERSTITIAL_MEDIUM, this);
                    RequestInterstitial();
                    interstitialMediumLoaded = false;
                }

                if (interstitialLowLoaded)
                {
                    Show(INTERSTITIAL_LOW, this);
                    RequestInterstitial();
                    interstitialLowLoaded = false;
                }
#endif
            }

            RequestInterstitial();
        }

        private void RequestInterstitial()
        {
#if UNITY_ADV
            Load(INTERSTITIAL_HIGH, this);
            Load(INTERSTITIAL_MEDIUM, this);
            Load(INTERSTITIAL_LOW, this);
#elif ADMOB
            LoadInterstitial(INTERSTITIAL_HIGH);
#endif
        }

        #endregion

        private void Load(string id, AdService service)
        {
#if UNITY_ADV
            Load(id, service);
#elif ADMOB

#endif
        }
#if ADMOB
        private void LoadRewarded(string id)
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }

            Debug.Log("Loading the rewarded ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            RewardedAd.Load(id, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    rewardHighLoaded = true;

                    RewardPoint.instance.RewardLoad();
                    RewardExp.instance.RewardLoad();
                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    _rewardedAd = ad;
                });
        }

        private void LoadInterstitial(string id)
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");

            // create our request used to load the ad.
            var adRequest = new AdRequest();


            // send the request to load the ad.
            InterstitialAd.Load(id, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    interstitialHighLoaded = true;

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                });
        }
#endif

        private void Show(string id, AdService service)
        {
#if UNITY_ADV
            Show(id, service);
#elif ADMOB
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    receive.Invoke();
                    receive = null;
                });
            }
#endif
        }

        private void ShowInterstitial(string id)
        {
#if ADMOB
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
#endif
        }
#if UNITY_ADV
        public void OnUnityAdsAdLoaded(string placementId)
        {
            switch (placementId)
            {
                case REWARD_HIGH:
                    rewardHighLoaded = true;
                    RewardPoint.instance.RewardLoad();
                    RewardExp.instance.RewardLoad();
                    break;
                case REWARD_MEDIUM:
                    rewardMediumLoaded = true;
                    RewardPoint.instance.RewardLoad();
                    RewardExp.instance.RewardLoad();
                    break;
                case REWARD_LOW:
                    rewardLowLoaded = true;
                    RewardPoint.instance.RewardLoad();
                    RewardExp.instance.RewardLoad();
                    break;
                case INTERSTITIAL_HIGH:
                    interstitialHighLoaded = true;
                    break;
                case INTERSTITIAL_MEDIUM:
                    interstitialMediumLoaded = true;
                    break;
                case INTERSTITIAL_LOW:
                    interstitialLowLoaded = true;
                    break;
            }
            Debug.Log("Ad was loaded with id: " +placementId);
        }
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError("Fallen to load ad: "+ placementId+" with error "+ error+" and message "+message);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            receive.Invoke();
            receive = null;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            RequestRewarded();
            RequestBanner();
            RequestInterstitial();

            TimerAds();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
#elif ADMOB
        private void OnApplicationQuit()
        {
            _rewardedAd.Destroy();
            _interstitialAd.Destroy();
            DestroyAd();
        }
        public void DestroyAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }
#endif
    }
}