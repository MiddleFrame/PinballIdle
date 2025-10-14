using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Popup : MonoBehaviour
{
    public bool fade = true;

    public delegate void PopupEvents(Popup popup);

    public static event PopupEvents OnOpenPopup;
    public static event PopupEvents OnClosePopup;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private RectTransform _popupTransform;

    [SerializeField]
    protected Button closeButton;

    private float _rightPosX;
    private const float MiddlePosX = 0;


    private const float TweenDuration = 0.5f;

    private Action _onShowAction;
    private Action _onCloseAction;
    private Action _onAfterCloseAction;


    protected virtual void Awake()
    {
        _rightPosX = _popupTransform.rect.width + Screen.width;
        _canvasGroup = GetComponent<CanvasGroup>();
        if (closeButton != null) closeButton.onClick.AddListener(Close);
    }

    public void Open(Action onShow = null, Action onClose = null, Action afterClose = null)
    {
        if (onShow != null)
            _onShowAction = onShow;
        if (onClose != null)
            _onCloseAction = onClose;
        if (afterClose != null)
            _onAfterCloseAction = onClose;

        OnOpenPopup?.Invoke(this);
        PlayShowAnimation();
    }

    public void Show()
    {
        _canvasGroup.DOFade(1, 0.1f);
    }

    public virtual void Hide()
    {
        _canvasGroup.DOFade(0, 0.5f);
    }

    public virtual void Close()
    {
        CloseAnimationSound();
        StartCoroutine(PopupHideAnimation());
    }


    protected virtual void ShowAnimationSound()
    {
    }

    protected virtual void AfterShowAnimation()
    {
        ShowAnimationSound();
        _onShowAction?.Invoke();
    }

    protected virtual void CloseAnimationSound()
    {
    }

    protected virtual void AfterHideAnimation()
    {
        _onCloseAction?.Invoke();
        Destroy(gameObject);
    }

    protected virtual void OnDisable()
    {
        _onAfterCloseAction?.Invoke();
        DOTween.Kill(gameObject);
    }


    private void PlayShowAnimation()
    {
        StartCoroutine(PopupShowAnimation());
    }

    private IEnumerator PopupShowAnimation()
    {
        Show();
        var _position = _popupTransform.position;
        _position = new Vector3(-_rightPosX, _position.y, _position.z);
        _popupTransform.position = _position;
        yield return _popupTransform.DOAnchorPosX(MiddlePosX, TweenDuration, true).SetUpdate(true).WaitForCompletion();
        AfterShowAnimation();
    }

    private IEnumerator PopupHideAnimation()
    {
        yield return null;
        OnClosePopup?.Invoke(this);
        yield return _popupTransform.DOAnchorPosX(_rightPosX, TweenDuration, true).SetUpdate(true).WaitForCompletion();
        
        AfterHideAnimation();
    }
}