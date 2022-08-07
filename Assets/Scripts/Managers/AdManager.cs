using System;
using System.Collections;
using YandexMobileAds;
using YandexMobileAds.Base;
using UnityEngine;
using UnityEngine.Events;
using YG;

namespace Managers
{
    public class AdManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private static AdManager instance;
        public static bool isNeedInterstitial = false;

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
            RequestRewarded();
            StartCoroutine(TimerInterstitial());
        }

        private IEnumerator TimerInterstitial()
        {
            while (true)
            {
                yield return new WaitForSeconds(300f);
                isNeedInterstitial = true;
            }
        }

        #region ExpReward

        private void RequestRewarded()
        {
            YandexGame.CloseVideoEvent += HandleUserEarnedReward;
            YandexGame.CheaterVideoEvent+= () =>
            {
                receive = null;
            };
        }

        private void HandleUserEarnedReward(int id)
        {
            if (id == 2)
            {
                RewardExp.instance.OnAdReceivedRewardExp();
            }
            else if (id == 1)
            {
                RewardPoint.instance.OnAdReceivedRewardX2();
            }
            else
            {
                receive.Invoke();
                receive = null;
            }
        }

        #endregion

        #region OtherReward

        private static UnityEvent receive;

        public static void ShowRewardOther(UnityEvent action)
        {
            if (AdsAndIAP.isRemoveAds)
            {
                action.Invoke();
                return;
            }

            receive = action;
            YG.YandexGame.RewVideoShow(0);
        }

        #endregion

        #region Interstitial

        public static void ShowInterstitial()
        {
            if (AdsAndIAP.isRemoveAds) return;
                YG.YandexGame.FullscreenShow();
        }

        #endregion
    }
}