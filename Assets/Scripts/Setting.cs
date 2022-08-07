using System;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public static MyPlayerSettings settings;

    [SerializeField]
    private Image _soundImage;

    [SerializeField]
    private Sprite _soundOn;

    [SerializeField]
    private Sprite _soundOff;

    [SerializeField]
    private Image _numberImage;

    [SerializeField]
    private Sprite _numberOn;

    [SerializeField]
    private Sprite _numberOff;

    [SerializeField]
    private GameObject _levelText;

    [SerializeField]
    private Image _levelTextImage;

    [SerializeField]
    private Sprite _levelTextOn;

    [SerializeField]
    private Sprite _levelTextOff;

    [SerializeField]
    private Image _vibrationImage;

    [SerializeField]
    private Sprite _vibrationOn;

    [SerializeField]
    private Sprite _vibrationOff;

    private void Start()
    {
        YG.YandexGame.GetDataEvent += () =>
        {
            if (!settings.exNum)
            {
                settings.exNum = !settings.exNum;
                ChangeNum();
            }

            if (!settings.sound)
            {
                settings.sound = !settings.sound;
                ChangeSound();
            }

            if (!settings.levelText)
            {
                settings.levelText = !settings.levelText;
                ViewLevelText();
            }

            if (!settings.vibration)
            {
                settings.vibration = !settings.vibration;
                ChangeVibration();
            }
        };
    }

    public void ChangeNum()
    {
        settings.exNum = !settings.exNum;
        _numberImage.sprite = settings.exNum ? _numberOn : _numberOff;
    }

    public void ChangeSound()
    {
        settings.sound = !settings.sound;

        if (!settings.sound)
        {
            AudioListener.volume = 0;
            _soundImage.sprite = _soundOff;
        }
        else
        {
            _soundImage.sprite = _soundOn;
            AudioListener.volume = 1;
        }
    }

    public void ChangeVibration()
    {
        settings.vibration = !settings.vibration;

        _vibrationImage.sprite = !settings.vibration ? _vibrationOff : _vibrationOn;
    }

    public void ViewLevelText()
    {
        settings.levelText = !settings.levelText;
        if (settings.levelText)
        {
            _levelTextImage.sprite = _levelTextOn;
            _levelText.SetActive(true);
        }
        else
        {
            _levelTextImage.sprite = _levelTextOff;
            _levelText.SetActive(false);
        }
    }
}

[Serializable]
public class MyPlayerSettings
{
    public bool exNum = true;
    public bool sound = true;
    public bool levelText = true;
    public bool vibration = true;
}