using Controllers;
using Yodo1.MAS;
using UnityEngine;

public class RewardDiamonds : MonoBehaviour
{
  

    public static void OnAdReceivedRewardDiamond()
    {
        PlayerDataController.Gems += 10;
    }
}
