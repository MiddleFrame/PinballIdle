using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoBehaviour
{
    [SerializeField]
    private Image _themeImage;

    [SerializeField]
    private Sprite _themeLightSprite;

    [SerializeField]
    private Sprite _themeDarkSprite;

    [SerializeField]
    private List<SpriteRenderer> _accent;

    [SerializeField]
    private List<SpriteRenderer> _field;

    [SerializeField]
    private List<Graphic> _fieldUI;

    [SerializeField]
    private List<Graphic> _text;
    
    [SerializeField]
    private List<TextMesh> _textMesh;

    [SerializeField]
    private List<SpriteRenderer> _black;

    [SerializeField]
    private List<SpriteRenderer> _gray;

    [SerializeField]
    private List<Graphic> _light;

    [SerializeField]
    private List<SpriteRenderer> _dark;

    [SerializeField]
    private List<Graphic> _darkest;

    [Space(20), ItemNotNull]
    public Theme[] themes;

    public static int currentTheme;
    public static ThemeManager instance;
    public static Action changeTheme;

    private static int CurrentTheme
    {
        get => PlayerPrefs.GetInt("theme", 0);
        set => PlayerPrefs.SetInt("theme", value);
    }

    private void Awake()
    {
        instance = this;
        currentTheme = CurrentTheme;
    }

    private void Start()
    {
        if (currentTheme == 0) return;
        ChangeThemes(currentTheme);
        _themeImage.sprite = _themeDarkSprite;
    }

    public void ChangeTheme()
    {
        ChangeThemes();
        _themeImage.sprite = currentTheme == 0 ? _themeLightSprite : _themeDarkSprite;
    }


    private void ChangeThemes(int i = -1)
    {
        if (i == -1)
        {
            CurrentTheme = currentTheme == 0 ? 1 : 0;
            currentTheme = currentTheme == 0 ? 1 : 0;
        }
        else
        {
            CurrentTheme = i;
            currentTheme = i;
        }

        if (Camera.main != null) Camera.main.backgroundColor = themes[currentTheme].accentColor;

        foreach (var _graphic in _accent)
        {
            _graphic.color = themes[currentTheme].accentColor;
        }

        foreach (var _graphic in _field)
        {
            _graphic.color = themes[currentTheme].fieldColor;
        }

        foreach (var _graphic in _fieldUI)
        {
            _graphic.color = themes[currentTheme].fieldColor;
        }

        foreach (var _graphic in _text)
        {
            _graphic.color = themes[currentTheme].textColor;
        }
        
        foreach (var _graphic in _textMesh)
        {
            _graphic.color = themes[currentTheme].textColor;
        }

        foreach (var _graphic in _black)
        {
            _graphic.color = themes[currentTheme].textColor;
        }

        foreach (var _graphic in _dark)
        {
            _graphic.color = themes[currentTheme].darkGray;
        }

        foreach (var _graphic in _darkest)
        {
            _graphic.color = themes[currentTheme].darkestGray;
        }

        foreach (var _graphic in _gray)
        {
            _graphic.color = themes[currentTheme].middleGray;
        }

        foreach (var _graphic in _light)
        {
            _graphic.color = themes[currentTheme].lightGray;
        }

        changeTheme?.Invoke();
    }
}

[Serializable]
public class Theme
{
    public Color accentColor;
    public Color fieldColor;
    public Color textColor;
    public Color lightGray;
    public Color darkGray;
    public Color middleGray;
    public Color darkestGray;
}