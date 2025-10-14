using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : Popup
{
    [SerializeField]
    private Statistics _statistics;
    [SerializeField]
    private Image _soundImage;

    [SerializeField]
    private Image _soundOffImage;

    [SerializeField]
    private Image _numberImage;

    [SerializeField]
    private Image _numberOffImage;

    [SerializeField]
    private Image _levelTextImage;

    [SerializeField]
    private Image _levelTextOffImage;

    [SerializeField]
    private Image _vibrationImage;

    [SerializeField]
    private Image _vibrationOffImage;

    [SerializeField]
    private Button _changeNum;

    [SerializeField]
    private Button _changeSound;

    [SerializeField]
    private Button _changeVibration;

    [SerializeField]
    private Button _changeViewText;

    [SerializeField]
    private Button _discord;

    [SerializeField]
    private Button _openStats;

    [SerializeField]
    private Button _cupCoffee;

    [SerializeField]
    private Button _noAds;

    
    private void Start()
    {
        Setting.Init(this);
        Init();
        _changeNum.onClick.AddListener(ChangeNum);
        _changeSound.onClick.AddListener(ChangeSound);
        _changeVibration.onClick.AddListener(ChangeVibration);
        _changeViewText.onClick.AddListener(ViewLevelText);
        _discord.onClick.AddListener(()=>Application.OpenURL(Setting.DISCORD_URL));
        _openStats.onClick.AddListener(_statistics.Open);
        _cupCoffee.onClick.AddListener(AdsAndIAP.BuyCoffee);
        _noAds.onClick.AddListener(AdsAndIAP.BuyRemoveAds);
    }

    private void Init()
    {
        _numberImage.enabled = Setting.ExNum;
        _numberOffImage.enabled = !Setting.ExNum;
        _soundImage.enabled = Setting.Sound;
        _soundOffImage.enabled = !Setting.Sound;
        _vibrationImage.enabled = Setting.Vibration;
        _vibrationOffImage.enabled = !Setting.Vibration;
        _levelTextImage.enabled = Setting.LevelText;
        _levelTextOffImage.enabled = !Setting.LevelText;
    }

    public void ChangeNum()
    {
        Setting.ExNum = !Setting.ExNum;
        _numberImage.enabled = Setting.ExNum;
        _numberOffImage.enabled = !Setting.ExNum;
    }

    public void ChangeSound()
    {
        Setting.Sound = !Setting.Sound;

        _soundImage.enabled = Setting.Sound;
        _soundOffImage.enabled = !Setting.Sound;
        AudioListener.volume = Setting.Sound?1:0;
    }

    public void ChangeVibration()
    {
        Setting.Vibration = !Setting.Vibration;

        _vibrationImage.enabled = Setting.Vibration;
        _vibrationOffImage.enabled = !Setting.Vibration;
    }

    public void ViewLevelText()
    {
        Setting.LevelText = !Setting.LevelText;

        _levelTextImage.enabled = Setting.LevelText;
        _levelTextOffImage.enabled = !Setting.LevelText;
    }

    private void OnDestroy()
    {
        _changeNum.onClick.RemoveListener(ChangeNum);
        _changeSound.onClick.RemoveListener(ChangeSound);
        _changeVibration.onClick.RemoveListener(ChangeVibration);
        _changeViewText.onClick.RemoveListener(ViewLevelText);
        _discord.onClick.RemoveListener(()=>Application.OpenURL(Setting.DISCORD_URL));
        _openStats.onClick.RemoveListener(_statistics.Open);
        _cupCoffee.onClick.RemoveListener(AdsAndIAP.BuyCoffee);
        _noAds.onClick.RemoveListener(AdsAndIAP.BuyRemoveAds);
    }
}