using Controllers;
using Yodo1.MAS;
using UnityEngine;

public class RewardDiamonds : MonoBehaviour
{
    #region Reward video Ad Methods

    private void DeleteReward()
    {
        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent -= OnAdReceivedRewardEvent;
    }

    private void DeleteReward(Yodo1U3dAdError adError)
    {
        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent -= OnAdReceivedRewardEvent;
    }

    public void InitializeRewardedAds()
    {
        Yodo1U3dMasCallback.Rewarded.OnAdClosedEvent += DeleteReward;
        Yodo1U3dMasCallback.Rewarded.OnAdErrorEvent += DeleteReward;
        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent += OnAdReceivedRewardEvent;
    }


    private void OnAdReceivedRewardEvent()
    {
        Debug.Log(Yodo1U3dMas.TAG + "Rewarded ad received reward");
        OnAdReceivedRewardDiamond();

        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent -= OnAdReceivedRewardEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdClosedEvent -= DeleteReward;
        Yodo1U3dMasCallback.Rewarded.OnAdErrorEvent -= DeleteReward;
    }

    #endregion


    private static void OnAdReceivedRewardDiamond()
    {
        PlayerDataController.Gems += 10;
    }
}
