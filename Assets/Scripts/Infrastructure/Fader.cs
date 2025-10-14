using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private Image fader;

    [SerializeField]
    private Button _fadeButton;


    private bool _isFade;

    private const float FadeTime = .5f;
    private float _currentAlpha = 0f;
    private const float MinValue = 0;
    private const float MaxValue = .4f;


    public void Init(UnityAction action)
    {
        _fadeButton.onClick.AddListener(action);
    }

    public bool IsFaded()
    {
        return _currentAlpha >= MaxValue;
    }

    public void FadeIn()
    {
        _isFade = true;
        if (fader != null)
        {
            fader.gameObject.SetActive(true);
        }
    }

    public void FadeOut()
    {
        _isFade = false;
    }

    void Update()
    {
        if (_isFade)
        {
            if (_currentAlpha > MaxValue)
                return;
            _currentAlpha += Time.deltaTime / FadeTime;
        }
        else
        {
            if (_currentAlpha < MinValue)
                return;
            _currentAlpha -= Time.deltaTime / FadeTime;
        }

        _currentAlpha = Mathf.Clamp(_currentAlpha, MinValue, MaxValue);
        fader.raycastTarget = _currentAlpha > 0;
        fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, _currentAlpha);
    }

    public void FadeAfterLoadingScene()
    {
        _currentAlpha = 1f;
        FadeOut();
    }
}