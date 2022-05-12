using System.Collections;
using Controllers;
using Managers;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Yodo1.MAS;

public class RewardPoint : MonoBehaviour
{
    //  private Graphic[] _lvlBuffs;
    [SerializeField]
    private Text _timex2Reward;

    [SerializeField]
    private GameObject _x2Bonus;

    [SerializeField]
    private Text _lvlBuffs;

    private static readonly bool[] reward = {false, false, false, false, false, false, false, false, false};
    public static int[] hitMultiply = {1, 1, 1, 1, 1, 1, 1, 1, 1};

    #region Reward video Ad Methods

    private void Awake()
    {
        FieldManager.openOneField += openNewField;
    }

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
        Debug.Log("Receive Reward");
        Debug.Log(Yodo1U3dMas.TAG + "Rewarded ad received reward");
        OnAdReceivedRewardX2();
        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent -= OnAdReceivedRewardEvent;

        Yodo1U3dMasCallback.Rewarded.OnAdClosedEvent -= DeleteReward;
        Yodo1U3dMasCallback.Rewarded.OnAdErrorEvent -= DeleteReward;
    }

    #endregion

    private void openNewField()
    {
        if (reward[FieldManager.currentField])
            return;
        _x2Bonus.SetActive(true);
        _timex2Reward.text = "";
        _lvlBuffs.text = $"x {PlayerDataController.playerStats.lvl[FieldManager.currentField]}";
        _lvlBuffs.color = ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
    }

    private void OnAdReceivedRewardX2()
    {
        hitMultiply[FieldManager.currentField] *= 2;
        StartCoroutine(Timex2());
    }

    private void changeColor()
    {
        if (!reward[FieldManager.currentField]) return;
        _lvlBuffs.color = new Color32(0xFB, 0xDE, 0x39, 0xFF);
    }

    private IEnumerator Timex2()
    {
        _x2Bonus.SetActive(false);

        int _field = FieldManager.currentField;
        reward[_field] = true;
        changeColor();
        ThemeManager.changeTheme += changeColor;
        _lvlBuffs.text =
            $"x {PlayerDataController.playerStats.lvl[_field] * hitMultiply[_field]}";
        GameManager.instance.fields[_field].stroke.SetActive(true);
        int _i = DefaultBuff.grade.bonusTime[_field];
        while (_i >= 1)
        {
            _i--;
            if (FieldManager.currentField == _field)
                _timex2Reward.text = _i.ToString();
            yield return new WaitForSeconds(1f);
        }

        hitMultiply[_field] /= 2;
        if (FieldManager.currentField == _field)
        {
            _timex2Reward.text = "";
            _lvlBuffs.text = $"x {PlayerDataController.playerStats.lvl[FieldManager.currentField]}";
            _lvlBuffs.color = ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
        }

        ThemeManager.changeTheme -= changeColor;

        GameManager.instance.fields[_field].stroke.SetActive(false);
        reward[_field] = false;
        yield return new WaitForSeconds(60f);
        if (FieldManager.currentField == _field)
        {
            _x2Bonus.SetActive(true);
        }
    }
}