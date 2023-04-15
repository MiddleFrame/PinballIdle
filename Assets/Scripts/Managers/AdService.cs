using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Advertisements.Advertisement;

namespace Managers
{
    public class AdService : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsInitializationListener,
        IUnityAdsShowListener
    {
        private string _gameId;
        private string _androidGameId;

        private const string BANNER_UNIT = "Banner_Android";
        private const string BANNER_UNIT_HIGH = "Banner_hight";
        private const string INTERSTITIAL_HIGH = "Interstitial_Hight";
        private const string INTERSTITIAL_MEDIUM = "Interstitial_Medium";
        private const string INTERSTITIAL_LOW = "Interstitial_auto";
        private const string REWARD_HIGH = "Hight_Rewarded";
        private const string REWARD_MEDIUM = "Medium_Rewarded";
        private const string REWARD_LOW = "Rewarded_Android";
        BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
        private bool rewardHighLoaded;
        private bool rewardMediumLoaded;
        private bool rewardLowLoaded;

        private bool interstitialHighLoaded;
        private bool interstitialMediumLoaded;
        private bool interstitialLowLoaded;

        private static int _time;

        private const string ID = "4265503";

        public static AdService instanse;
        private void Awake()
        {
            DontDestroyOnLoad(this);
            instanse = this;
            if (AdsAndIAP.isRemoveAds)
                return;
            _androidGameId = ID;
            InitializeAds();
            
        }

        private void InitializeAds()
        {
            _gameId = _androidGameId;

            if (!isInitialized && isSupported)
            {
                Initialize(_gameId, false, this);
            }
        }

        #region Reward

        private void RequestRewarded()
        {
            Load(REWARD_HIGH, this);
            Load(REWARD_MEDIUM, this);
            Load(REWARD_LOW, this);
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
                RequestRewarded();
            }
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
                    RequestRewarded();
                    rewardHighLoaded = false;
                }

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
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
            Banner.Show(BANNER_UNIT_HIGH, options);
            if (_higheBannerIsShowing) return;
            
            Banner.Show(BANNER_UNIT, options);
        }

        private void RequestBanner()
        {
            Banner.SetPosition(_bannerPosition);
            LoadBanner();
            Banner.Hide();
        } 
        
        public void LoadBanner()
        {
            // Set up options to notify the SDK of load events:
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };
 
            // Load the Ad Unit with banner content:
            Banner.Load(BANNER_UNIT_HIGH, options);
            Banner.Load(BANNER_UNIT, options);
        }

        private bool _higheBannerIsShowing;
        void OnBannerClicked() { }

        void OnBannerShown()
        {
            _higheBannerIsShowing = true;
            
        }
        void OnBannerHidden() { }
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
            Banner.Hide();
        }

        #endregion

        #region Interstitial

        public void ShowInterstitial()
        {
            _time = 0;
            if (AdsAndIAP.isRemoveAds) return;
            if (interstitialHighLoaded)
            {
                Show(INTERSTITIAL_HIGH, this);
                RequestInterstitial();
                interstitialHighLoaded = false;
            }
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
                    Show(INTERSTITIAL_HIGH, this);
                    RequestInterstitial();
                    interstitialHighLoaded = false;
                }

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
            }

            RequestInterstitial();
        }

        private void RequestInterstitial()
        {
            Load(INTERSTITIAL_HIGH, this);
            Load(INTERSTITIAL_MEDIUM, this);
            Load(INTERSTITIAL_LOW, this);
        }

        private async void TimerAds()
        {
            if (TutorialManager._isNeedTutorialRank)
                _time = -120;
            while (true)
            {
                _time++;
                await Task.Delay(1000);
                if (_time >= 120 && LetsScript.isCompetitive)
                    ShowInterstitial();
            }
        }

        #endregion

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
    }
}