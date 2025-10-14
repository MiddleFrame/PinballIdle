using System;
using UnityEngine;
using Zenject;

public class PopupsView : MonoInstaller
{
    [SerializeField]
    private Fader _fader;
    [SerializeField]
    private Canvas _canvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void InstallBindings()
    {
        Container.Bind<Fader>().FromInstance(_fader).AsSingle();
        Container.Bind<Canvas>().FromInstance(_canvas).AsSingle();
    }
}
