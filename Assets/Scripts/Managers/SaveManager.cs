using System;
using System.Collections;
using Controllers;
using Shop;
using UnityEngine;
using YG;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {

        private static bool isInit;
    
        private void Awake()
        {
            if (isInit)
            {
                Destroy(this);
                return;
            }

            YandexGame.GetDataEvent += LoadGame;
            YandexGame.GetDataEvent += ()=>
            {
                StartCoroutine(SavedGame());
            };
            isInit = true;
            DontDestroyOnLoad(this);
            Debug.Log("Init save manager "+isInit);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private IEnumerator SavedGame()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                SaveGame();
            }
        }
  
          private void OnApplicationPause(bool pause)
          {
              if (pause)
              {
                  SaveGame();
              }
          }

          private void OnApplicationFocus(bool hasFocus)
          {
              SaveGame();
          }

          private static void SaveGame()
        {
            Debug.Log("Save player data.");
            YandexGame.savesData.defaultBuff = DefaultBuff.grade;
            YandexGame.savesData.buyStopper = BuyStopper.grades;
            YandexGame.savesData.statistics = Statistics.stats;
            YandexGame.savesData.playerDataController = PlayerDataController.playerStats;
            YandexGame.savesData.setting = Setting.settings;
            YandexGame.savesData.fieldManager = FieldManager.fields;
            YandexGame.savesData.unlockCircles = UnlockCircles.upgrade;
            YandexGame.savesData.ballsManager = ChallengeManager.progress;
            YandexGame.savesData.challengeManager = BallsManager.balls;
            YandexGame.savesData.skinShopController = SkinShopController.skins;
            YandexGame.SaveProgress();
        }


        private static void LoadGame()
        {
            DefaultBuff.grade= YandexGame.savesData.defaultBuff;
            BuyStopper.grades=YandexGame.savesData.buyStopper;
            Statistics.stats=YandexGame.savesData.statistics ;
            PlayerDataController.playerStats= YandexGame.savesData.playerDataController ;
            Setting.settings=YandexGame.savesData.setting ;
            FieldManager.fields=YandexGame.savesData.fieldManager ;
            UnlockCircles.upgrade= YandexGame.savesData.unlockCircles ;
            ChallengeManager.progress= YandexGame.savesData.ballsManager;
            BallsManager.balls= YandexGame.savesData.challengeManager ;
            SkinShopController.skins=YandexGame.savesData.skinShopController  ;
        }
    }
}