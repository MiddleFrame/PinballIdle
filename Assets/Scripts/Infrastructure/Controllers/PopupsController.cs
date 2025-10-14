using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Managers;
using UnityEngine;
using Zenject;


public class PopupsController
{
    private readonly Fader _fader;
    private readonly List<Popup> _popupStack = new();
    private readonly Canvas _canvas;

    
    internal PopupsController(Canvas fieldCanvas, Fader fader)
    {
        _canvas = fieldCanvas;
        _fader = fader;
        _fader.Init(CloseAllPopups);
        _fader.FadeAfterLoadingScene();
        Popup.OnClosePopup += ClosePopup;
        FieldManager.openOneField += CloseAllPopups;
    }

    public void RegisterActionToOpenPopup<TPopup>(Action onOpen) where TPopup : Popup
    {
        if (ImplementedPopup<TPopup>.onOpenAction == null || isSameActionNotInListener<TPopup>(onOpen))
            ImplementedPopup<TPopup>.onOpenAction += onOpen;
    }

    public TPopup ShowPopup<TPopup>(Action onShow = null, Action onClose = null) where TPopup : Popup
    {
        TPopup popupPrefab = Resources.Load<TPopup>("Popups/" + typeof(TPopup).Name);

        var popup = MonoBehaviour.Instantiate<TPopup>(popupPrefab, _canvas.transform);

        if (popup == null)
        {
            Debug.LogError("Popup prefab not found in Resources folder: " + typeof(TPopup).Name);
            return null;
        }

        if (_popupStack.Count > 0)
        {
            _popupStack.Last().Hide();
        }

        _popupStack.Add(popup);
        popup.Open(onShow, onClose);
        var rectTransform = popup.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
        if (_fader != null && _popupStack.Count > 0 && popup.fade)
            _fader.FadeIn();
        ImplementedPopup<TPopup>.onOpenAction?.Invoke();
        return popup;
    }

    public TPopup GetPopupOpened<TPopup>() where TPopup : Popup
    {
        foreach (var popup in _popupStack)
        {
            if (popup.GetType() == typeof(TPopup))
                return (TPopup) popup;
        }

        return null;
    }

    public bool isPopupOpened<TPopup>() where TPopup : Popup
        => _popupStack.Exists(popup => popup.GetType() == typeof(TPopup));
      
    
    private bool isSameActionNotInListener<TPopup>(Action onOpen) where TPopup : Popup =>
        ImplementedPopup<TPopup>.onOpenAction != null &&
         !ImplementedPopup<TPopup>.onOpenAction.GetInvocationList().Contains(onOpen);

    private void ClosePopup(Popup popupClose)
    {
        if (_popupStack.Count > 0)
        {
            _popupStack.Remove(popupClose);
            if (_popupStack.Count > 0)
            {
                var popup = _popupStack.Last();
                popup.Show();
            }
        }

        if (_fader != null && _popupStack.Count == 0 && _fader.IsFaded())
            _fader.FadeOut();
    }


    internal void CloseAllPopups()
    {
        foreach (var popup in _popupStack)
        {
            popup.Close();
        }

        _popupStack.Clear();
    }

    private static class ImplementedPopup<TPopup> where TPopup : Popup
    {
        internal static Action onOpenAction;
    }
}