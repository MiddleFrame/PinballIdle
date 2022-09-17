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
        private static InterstitialAd _interstitial;
        private static RewardedAd _rewardExp;
        private static RewardedAd _rewardX2;
        private static RewardedAd _rewardOther;
        private const string BANNER_UNIT ="ca-app-pub-8340576279106634/2182143650";
        private const string INTERSTITIAL_UNIT = "ca-app-pub-8340576279106634/8578093242";
        private const string REWARD_EXP_UNIT = "ca-app-pub-8340576279106634/8409189390";
        private const string REWARD_X2_UNIT = "ca-app-pub-8340576279106634/5205824445";
        private const string REWARD_OTHER = "ca-app-pub-8340576279106634/9874314306";
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
            MobileAds.Initialize(initStatus => { Debug.Log("Ads init with status "+initStatus); });
            
            RequestBanner();
            RequestInterstitial();
            RequestRewardedExp();
            RequestRewardedX2();
            RequestRewardedOther();
          
        }

        #region ExpReward

        private void RequestRewardedExp()
        {
            _rewardExp = new RewardedAd(REWARD_EXP_UNIT);
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardExp.OnAdLoaded += HandleRewardedAdLoadedExp;
            _rewardExp.OnUserEarnedReward += HandleUserEarnedRewardExp;
            _rewardExp.OnAdClosed += HandleRewardedAdClosedExp;
            _rewardExp.LoadAd(_request);
        }
        private void HandleRewardedAdClosedExp(object sender, EventArgs args)
        {
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardExp.LoadAd(_request);
        }      
        private void HandleUserEarnedRewardExp(object sender, EventArgs args)
        {
            RewardExp.instance.OnAdReceivedRewardExp();
        }       
        private void HandleRewardedAdLoadedExp(object sender, EventArgs args)
        {
            RewardExp.instance.RewardLoad();
        } 
        public static void ShowRewardExp()
        {
            _time = 0;
            if (AdsAndIAP.isRemoveAds)
            {
                RewardExp.instance.OnAdReceivedRewardExp();
                return;
            }
            if (_rewardExp.IsLoaded())
            {
                _rewardExp.Show();
            }
        }
        #endregion
        
        #region X2Reward

        private void RequestRewardedX2()
        {
            _rewardX2 = new RewardedAd(REWARD_X2_UNIT);
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardX2.OnAdLoaded += HandleRewardedAdLoadedX2;
            _rewardX2.OnUserEarnedReward += HandleUserEarnedRewardX2;
            _rewardX2.OnAdClosed += HandleRewardedAdClosedX2;
            _rewardX2.LoadAd(_request);
        }
        private void HandleRewardedAdClosedX2(object sender, EventArgs args)
        {
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardX2.LoadAd(_request);
        }      
        private void HandleUserEarnedRewardX2(object sender, EventArgs args)
        {
            RewardPoint.instance.OnAdReceivedRewardX2();
        }       
        private void HandleRewardedAdLoadedX2(object sender, EventArgs args)
        {
            RewardPoint.instance.RewardLoad();
        } 
        
        public static void ShowRewardX2()
        {
            if (AdsAndIAP.isRemoveAds)
            {
                RewardPoint.instance.OnAdReceivedRewardX2();
                return;
            }
            _time = 0;
            if (_rewardX2.IsLoaded())
            {
                _rewardX2.Show();
            }
        }
        #endregion
        
        #region OtherReward

        private void RequestRewardedOther()
        {
            _rewardOther = new RewardedAd(REWARD_OTHER);
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardOther.OnAdClosed += HandleRewardedAdClosedOther;
            _rewardOther.OnUserEarnedReward += HandleOnReceive;
            _rewardOther.LoadAd(_request);
        }
        private void HandleRewardedAdClosedOther(object sender, EventArgs args)
        {
            AdRequest _request = new AdRequest.Builder().Build();
            _rewardX2.LoadAd(_request);
        }     
        private static void HandleOnReceive(object sender, EventArgs args)
        {
            receive.Invoke();
            receive = null;
        }

        private static UnityEvent receive;
        public static void ShowRewardOther(UnityEvent action)
        {
            if (AdsAndIAP.isRemoveAds)
            {
                action.Invoke();
                return;
            }

            _time = 0;
            if (_rewardX2.IsLoaded())
            {
                receive = action;
                _rewardOther.Show();
            }
        }
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
            if (_interstitial.IsLoaded())
            {
                _interstitial.Show();
                AdRequest _request = new AdRequest.Builder().Build();
                _interstitial.LoadAd(_request);
            }
            else
            {
               instance.StartCoroutine(TryToShowInterstitial());
            }
        }

        private static IEnumerator TryToShowInterstitial()
        {
            int _i = 0;
            while (!_interstitial.IsLoaded())
            {
                _i++;
                yield return new WaitForSeconds(1f);
                if (_i == 5)
                {
                    yield break;
                }
            }

            _interstitial.Show();
        }
        private void RequestInterstitial()
        {
            _interstitial = new InterstitialAd(INTERSTITIAL_UNIT);
            AdRequest _request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            _interstitial.LoadAd(_request);
            StartCoroutine(TimerAds());
        }

        private IEnumerator TimerAds()
        {
            _time = -360;
            while (true)
            {
                _time++;
                yield return new WaitForSeconds(1f);
                if (_time == 180)
                {
                    ShowInterstitial();
                }
            }
        }
      
        #endregion
    }
}