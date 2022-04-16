using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public static PlayerSettings settings;
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
    private Image _vibroImage;
    [SerializeField]
    private Sprite _vibroOn;
    [SerializeField]
    private Sprite _vibroOff;
    private void Start()
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
        if (!settings.vibro)
        {
            settings.vibro = !settings.vibro;
            ChangeVibro();
        }
    }
    public void ChangeNum()
    {
       
            settings.exNum = !settings.exNum;
        if (settings.exNum)
        {
            _numberImage.sprite = _numberOn;
        }
        else
        {
            _numberImage.sprite = _numberOff;
        }
    }

    public void ChangeSound()
    {
       
            settings.sound= !settings.sound;
        
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
    public void ChangeVibro()
    {
       
            settings.vibro= !settings.vibro;
        
        if (!settings.vibro)
        {
            _vibroImage.sprite = _vibroOff;
        }
        else
        {

            _vibroImage.sprite = _vibroOn;
            
        }
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


public class PlayerSettings
{
    public bool exNum = true;
    public bool sound = true;
    public bool levelText = true;
    public bool vibro = true;
    public int theme = 0;
}