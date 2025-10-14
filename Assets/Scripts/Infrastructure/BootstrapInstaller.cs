using Controllers;
using Managers;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PopupsController>().AsSingle().Lazy();

        SaveManager.LoadGame();

        
    }


    
    private void OnApplicationQuit()
    {
      SaveManager.SaveGame();
    }

#if !UNITY_EDITOR
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveManager.SaveGame();
            }
            else
            {
                SaveManager.LoadGame();
            }
        }
#endif
}
