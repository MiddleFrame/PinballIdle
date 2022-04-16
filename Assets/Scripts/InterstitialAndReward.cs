using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Yodo1.MAS;

public class InterstitialAndReward : MonoBehaviour
{
    [SerializeField]
    private Text _timeExpReward;
    [SerializeField]
    private GameObject _expBonus;
    [SerializeField]
    private Graphic[] _lvlBuffs;
    [SerializeField]
    private Text _timex2Reward;
    [SerializeField]
    private GameObject _x2Bonus;
    [SerializeField]
    private Text _lvlxBuffs;

    public static int hitMultiply = 1;
    private void Start()
    {
        Yodo1AdBuildConfig config = new Yodo1AdBuildConfig().enableUserPrivacyDialog(true);
        Yodo1U3dMas.SetAdBuildConfig(config);
            Yodo1U3dMas.InitializeSdk();
        
       
    }
    
    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (interstitial == null)
            {
                interstitial = StartCoroutine(Interstitial());
            }
        }
    }

    Coroutine interstitial = null;
    public static int timeOutReward = 120;
    public static readonly int timeOutRewardMax = 120;

    IEnumerator Interstitial()
    {
        timeOutReward = timeOutRewardMax;
        while (true)
        {
            timeOutReward--;
            yield return new WaitForSeconds(1f);
            if(timeOutReward < 0)
            {
                ShowInterstitial();
                timeOutReward = timeOutRewardMax;
            }
        }
    }
    public void ShowInterstitial()
    {
        if (!AdsAndIAP.isRemoveADS)
        {
            bool isLoaded = Yodo1U3dMas.IsInterstitialAdLoaded();
            if (isLoaded)
            {
                Yodo1U3dMas.ShowInterstitialAd();
            }
        }
    }


    


    public void OnAdReceivedRewardExp()
    {
        LetsScript.exp *= 2;
        timeOutReward = timeOutRewardMax;
        StartCoroutine(TimeExp());

    } 

    public void OnAdReceivedRewardx2()
    {
        hitMultiply *= 2;
        timeOutReward = timeOutRewardMax;
        StartCoroutine(Timex2());
    }

    IEnumerator TimeExp()
    {
        _expBonus.SetActive(false);
        for (int j = 0; j< _lvlBuffs.Length; j++)
        {
            _lvlBuffs[j].color = Color.yellow;
        }
        int i = 30;
        while (i>=0)
        {
            i--;
            _timeExpReward.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        LetsScript.exp /= 2;
        _timeExpReward.text = "";
        for (int j = 0; j < _lvlBuffs.Length; j++)
        {
            _lvlBuffs[j].color = Color.black;
        }

        yield return new WaitForSeconds(60f);
        _expBonus.SetActive(true);
    }
    
    IEnumerator Timex2()
    {
        _x2Bonus.SetActive(false);
        _lvlxBuffs.text = $"x {PlayerDataController.Lvl * hitMultiply}";
            _lvlxBuffs.color = Color.yellow;
        
        int i = 30;
        while (i>=0)
        {
            i--;
            _timex2Reward.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        hitMultiply /= 2;
        _timex2Reward.text = "";
        _lvlxBuffs.text = $"x {PlayerDataController.Lvl}";
        _lvlxBuffs.color = Color.black;
        

        yield return new WaitForSeconds(60f);
        _x2Bonus.SetActive(true);
    }


    

}
