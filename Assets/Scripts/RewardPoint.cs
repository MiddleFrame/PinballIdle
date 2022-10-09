using System.Collections;
using Controllers;
using Managers;
using Shop;
using UnityEngine;
using UnityEngine.UI;
public class RewardPoint : MonoBehaviour
{
    //  private Graphic[] _lvlBuffs;
    
    public static RewardPoint instance;
    [SerializeField]
    private Text _timex2Reward;

    [SerializeField]
    private GameObject _x2Bonus;

    [SerializeField]
    private Text _lvlBuffs;

    private static readonly bool[] reward = {false, false, false, false, false, false, false, false, false};
    public static int[] hitMultiply = {1, 1, 1, 1, 1, 1, 1, 1, 1};


    private bool _isAfterReward;
    private void Awake()
    {
        instance = this;
        FieldManager.openOneField += openNewField;
    }

    private void openNewField()
    {
        if (reward[FieldManager.currentField])
        {
            changeColor();
            _x2Bonus.SetActive(false);_lvlBuffs.text =
            $"x {PlayerDataController.playerStats.lvl[FieldManager.currentField] * hitMultiply[FieldManager.currentField]}";
            return;
        }

        _x2Bonus.SetActive(true);
        _timex2Reward.text = "";
        _lvlBuffs.text = $"x {PlayerDataController.playerStats.lvl[FieldManager.currentField]}";
        _lvlBuffs.color = ThemeManager.instance.themes[ThemeManager.currentTheme].textColor;
    }

    public void OnAdReceivedRewardX2()
    {
        hitMultiply[FieldManager.currentField] *= 2;
        StartCoroutine(Timex2());
    }
    public void RewardLoad()
    {
        if(!_isAfterReward)
            _x2Bonus.SetActive(true);
    }
    private void changeColor()
    {
        if (!reward[FieldManager.currentField]) return;
        _lvlBuffs.color = new Color32(0xFB, 0xDE, 0x39, 0xFF);
    }

    private IEnumerator Timex2()
    {
        _isAfterReward = true;
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
        
        _isAfterReward = false;
        if (FieldManager.currentField == _field)
        {
            _x2Bonus.SetActive(true);
        }
    }
}