using System.Collections;
using Managers;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Yodo1.MAS;

public class RewardExp : MonoBehaviour
{
    [SerializeField]
    private Text _timeExpReward;

    [SerializeField]
    private GameObject _expBonus;

    [SerializeField]
    private Graphic[] _lvlBuffs;

    private static readonly bool[] reward = {false, false, false, false, false, false, false, false, false};
    
    private void Awake()
    {
        FieldManager.openOneField += openNewField;
    }
    
    
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
        OnAdReceivedRewardExp();

        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent -= OnAdReceivedRewardEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdClosedEvent -= DeleteReward;
        Yodo1U3dMasCallback.Rewarded.OnAdErrorEvent -= DeleteReward;
    }

    #endregion


    private void OnAdReceivedRewardExp()
    {
        LetsScript.exp *= 2;
        StartCoroutine(timeExp());
    }

    private void changeColor()
    {
        if (!reward[FieldManager.currentField]) return;
        foreach (var _t in _lvlBuffs)
        {
            _t.color = new Color32(0xFB, 0xDE, 0x39, 0xFF);
        }
    }

    private void openNewField()
    {
        if (reward[FieldManager.currentField])
        {
            _expBonus.SetActive(false);
            changeColor();
            return;
        }

        _expBonus.SetActive(true);
        _timeExpReward.text = "";

        foreach (var _t in _lvlBuffs)
        {
            _t.color = ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
        }
    }

    private IEnumerator timeExp()
    {
        _expBonus.SetActive(false);
        int _field = FieldManager.currentField;
        reward[_field] = true;
        changeColor();
        ThemeManager.changeTheme += changeColor;
        int _i = DefaultBuff.grade.expTime[FieldManager.currentField];
        while (_i >= 1)
        {
            _i--;
            if (FieldManager.currentField == _field)
                _timeExpReward.text = _i.ToString();
            yield return new WaitForSeconds(1f);
        }

        LetsScript.exp /= 2;
        if (FieldManager.currentField == _field)
        {
            _timeExpReward.text = "";
            foreach (var _t in _lvlBuffs)
            {
                _t.color = ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
            }
        }
        reward[_field] = false;
        ThemeManager.changeTheme -= changeColor;


        yield return new WaitForSeconds(60f);
        _expBonus.SetActive(true);
    }
}