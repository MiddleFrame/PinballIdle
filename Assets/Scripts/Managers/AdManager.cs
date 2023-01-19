using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class AdManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private static AdManager instance;
        private static BannerView _bannerView;
        private static InterstitialAd _interstitialHigh;
        private static InterstitialAd _interstitialMedium;
        private static InterstitialAd _interstitialLow;
        private static RewardedAd _rewardedHigh;
        private static RewardedAd _rewardMedium;
        private static RewardedAd _rewardLow;
        private const string BANNER_UNIT = "ca-app-pub-8340576279106634/2182143650";
        private const string INTERSTITIAL_HIGH = "ca-app-pub-8340576279106634/8578093242";
        private const string INTERSTITIAL_MEDIUM = "ca-app-pub-8340576279106634/8828547016";
        private const string INTERSTITIAL_LOW = "ca-app-pub-8340576279106634/5495806660";
        private const string REWARD_HIGH = "ca-app-pub-8340576279106634/5205824445";
        private const string REWARD_MEDIUM = "ca-app-pub-8340576279106634/9874314306";
        private const string REWARD_LOW = "ca-app-pub-8340576279106634/8409189390";
        private static int _time;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            if (AdsAndIAP.isRemoveAds)
                return;
            MobileAds.Initialize(initStatus => { Debug.Log("Ads init with status " + initStatus); });

            RequestBanner();
            RequestInterstitial();
            RequestRewardedHigh();
            RequestRewardedMedium();
            RequestRewardedLow();
            
            StartCoroutine(TimerAds());
        }

        #region Reward

        private static void RequestRewardedHigh()
        {
            _rewardedHigh = new RewardedAd(REWARD_HIGH);
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardedHigh.OnAdLoaded += HandleRewardedAdLoadedExp;
            _rewardedHigh.OnAdLoaded += HandleRewardedAdLoadedX2;
            _rewardedHigh.OnUserEarnedReward += HandleOnReceive;
            _rewardedHigh.LoadAd(_request);
        }

        private static void RequestRewardedMedium()
        {
            _rewardMedium = new RewardedAd(REWARD_MEDIUM);
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardMedium.OnAdLoaded += HandleRewardedAdLoadedX2;
            _rewardMedium.OnAdLoaded += HandleRewardedAdLoadedExp;
            _rewardMedium.OnUserEarnedReward += HandleOnReceive;
            _rewardMedium.LoadAd(_request);
        }

        private static void RequestRewardedLow()
        {
            _rewardLow = new RewardedAd(REWARD_LOW);
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardLow.OnAdLoaded += HandleRewardedAdLoadedX2;
            _rewardLow.OnAdLoaded += HandleRewardedAdLoadedExp;
            _rewardLow.OnUserEarnedReward += HandleOnReceive;
            _rewardLow.LoadAd(_request);
        }


        private static void HandleRewardedAdLoadedExp(object sender, EventArgs args)
        {
            RewardExp.instance.RewardLoad();
        }

        public static void ShowReward(UnityEvent unityAction)
        {
            _time = 0;
            if (AdsAndIAP.isRemoveAds)
            {
                unityAction.Invoke();
                return;
            }

            if (_rewardedHigh.IsLoaded())
            {
                receive = unityAction;
                _rewardedHigh.Show();
                AdRequest _request = new AdRequest.Builder().Build();
                _rewardedHigh.LoadAd(_request);
            }
            else if (_rewardMedium.IsLoaded())
            {
                receive = unityAction;
                _rewardMedium.Show();
                AdRequest _request = new AdRequest.Builder().Build();
                _rewardMedium.LoadAd(_request);
            }
            else if (_rewardLow.IsLoaded())
            {
                receive = unityAction;
                _rewardLow.Show();
                AdRequest _request = new AdRequest.Builder().Build();
                _rewardLow.LoadAd(_request);
            }
            else
            {
                GameManager.instance.StartCoroutine(TryToShowRewarded(unityAction));
            }
            
        }

        private static IEnumerator TryToShowRewarded(UnityEvent unityAction)
        {
            for (int _i = 0; _i < 5; _i++)
            {
                yield return new WaitForSeconds(0.3f);
                if (_rewardedHigh.IsLoaded())
                {
                    receive = unityAction;
                    _rewardedHigh.Show();
                    AdRequest _request = new AdRequest.Builder().Build();
                    _rewardedHigh.LoadAd(_request);
                    yield break;
                }

                if (_rewardMedium.IsLoaded())
                {
                    receive = unityAction;
                    _rewardMedium.Show();
                    AdRequest _request = new AdRequest.Builder().Build();
                    _rewardMedium.LoadAd(_request);
                    yield break;
                }

                if (_rewardLow.IsLoaded())
                {
                    receive = unityAction;
                    _rewardLow.Show();
                    AdRequest _request = new AdRequest.Builder().Build();
                    _rewardLow.LoadAd(_request);
                    yield break;
                }
            }
        }


        private static void HandleRewardedAdLoadedX2(object sender, EventArgs args)
        {
            RewardPoint.instance.RewardLoad();
        }
        

        private static void HandleOnReceive(object sender, EventArgs args)
        {
            receive.Invoke();
            receive = null;
        }

        private static UnityEvent receive;

        #endregion

        #region Banner

        public static void ShowBanner()
        {
            if (AdsAndIAP.isRemoveAds) return;
            _bannerView.Show();
        }

        private void RequestBanner()
        {
            // Create a 320x50 banner at the top of the screen.
            _bannerView = new BannerView(BANNER_UNIT, AdSize.Banner, AdPosition.Bottom);
            AdRequest _request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            _bannerView.LoadAd(_request);
            _bannerView.Hide();
        }

        public static void HideBanner()
        {
            if (AdsAndIAP.isRemoveAds) return;
            _bannerView.Hide();
        }

        #endregion

        #region Interstitial

        public static void ShowInterstitial()
        {
            if (AdsAndIAP.isRemoveAds) return;
            _time = 0;
            if (_interstitialHigh.IsLoaded())
            {
                _interstitialHigh.Show();
                AdRequest _request = new AdRequest.Builder().Build();
                _interstitialHigh.LoadAd(_request);
            }
            else if (_interstitialMedium.IsLoaded())
            {
                _interstitialMedium.Show();
                AdRequest _request = new AdRequest.Builder().Build();
                _interstitialMedium.LoadAd(_request);
            }
            else if (_interstitialLow.IsLoaded())
            {
                _interstitialLow.Show();
                AdRequest _request = new AdRequest.Builder().Build();
                _interstitialLow.LoadAd(_request);
            }
            else
            {
                instance.StartCoroutine(instance.TryToShowInterstitial());
            }
        }

        private  IEnumerator TryToShowInterstitial()
        {
            for (int _i = 0; _i < 5; _i++)
            {
                yield return new WaitForSeconds(0.3f);
                if (_interstitialHigh.IsLoaded())
                {
                    _interstitialHigh.Show();
                    AdRequest _request = new AdRequest.Builder().Build();
                    _interstitialHigh.LoadAd(_request);
                    yield break;
                }

                if (_interstitialMedium.IsLoaded())
                {
                    _interstitialMedium.Show();
                    AdRequest _request = new AdRequest.Builder().Build();
                    _interstitialMedium.LoadAd(_request);
                    yield break;
                }

                if (_interstitialLow.IsLoaded())
                {
                    _interstitialLow.Show();
                    AdRequest _request = new AdRequest.Builder().Build();
                    _interstitialLow.LoadAd(_request);
                    yield break;
                }
            }

            RequestInterstitial();
        }

        private void RequestInterstitial()
        {
            _interstitialHigh = new InterstitialAd(INTERSTITIAL_HIGH);
            _interstitialMedium = new InterstitialAd(INTERSTITIAL_MEDIUM);
            _interstitialLow = new InterstitialAd(INTERSTITIAL_LOW);
            AdRequest _requestHigh = new AdRequest.Builder().Build();
            AdRequest _requestMedium = new AdRequest.Builder().Build();
            AdRequest _requestLow = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            _interstitialHigh.LoadAd(_requestHigh);
            _interstitialMedium.LoadAd(_requestMedium);
            _interstitialLow.LoadAd(_requestLow);
        }

        private static IEnumerator TimerAds()
        {
            if (TutorialManager._isNeedTutorialRank)
                _time = -120;
            while (true)
            {
                _time++;
                yield return new WaitForSeconds(1f);
                if (_time == 120)
                {
                    ShowInterstitial();
                    _time = 0;
                }
            }
        }

        #endregion
    }
}