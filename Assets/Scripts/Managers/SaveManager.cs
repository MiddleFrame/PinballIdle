using System.Collections;
using Controllers;
using Shop;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        private static bool isInit;

        private static bool isLoading;
        private static SaveManager instance;

        private void Awake()
        {
            if (isInit)
            {
                Destroy(this);
                return;
            }

            isInit = true;
            instance = this;
            DontDestroyOnLoad(this);
            LoadGame();
            Debug.Log("Init save manager " + isInit);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

#if !UNITY_EDITOR
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveGame();
            }
            else
            {
                LoadGame();
            }
        }
#endif

        private static void SaveGame()
        {
            Debug.Log("Save player data.");
            PlayerPrefs.SetString("DefaultBuff", JsonUtility.ToJson(DefaultBuff.grade));
            PlayerPrefs.SetString("StoppersBuff", JsonUtility.ToJson(BuyStopper.grades));
            PlayerPrefs.SetString("Statistic", JsonUtility.ToJson(Statistics.stats));
            PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(PlayerDataController.playerStats));
            PlayerPrefs.SetString("Settings", JsonUtility.ToJson(Setting.settings));
            PlayerPrefs.SetString("Fields", JsonUtility.ToJson(FieldManager.fields));
            PlayerPrefs.SetString("UpgradeCircle", JsonUtility.ToJson(UnlockCircles.upgrade));
            PlayerPrefs.SetString("Challenge", JsonUtility.ToJson(ChallengeManager.progress));
            PlayerPrefs.SetString("BallsManager", JsonUtility.ToJson(BallsManager.balls));
            PlayerPrefs.SetString("Skins", JsonUtility.ToJson(SkinShopController.skins));
            PlayerPrefs.SetString("GlobalQuest", JsonUtility.ToJson(QuestManager.progress[0]));
            for (int _i = 0; _i < 9; _i++)
            {
                PlayerPrefs.SetString("Quest" + _i, JsonUtility.ToJson(QuestManager.progress[_i + 1]));
            }
        }


        private static void LoadGame()
        {
            if (isLoading) return;
            Debug.Log("Loading player data.");
            if (!PlayerPrefs.HasKey("version 2.3"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetFloat("version 2.3", 0);
            }

            DefaultBuff.grade =
                JsonUtility.FromJson<CostAndGrade>(PlayerPrefs.GetString("DefaultBuff",
                    JsonUtility.ToJson(new CostAndGrade())));
            BuyStopper.grades =
                JsonUtility.FromJson<StopperGrades>(PlayerPrefs.GetString("StoppersBuff",
                    JsonUtility.ToJson(new StopperGrades(9))));
            Statistics.stats =
                JsonUtility.FromJson<Stats>(PlayerPrefs.GetString("Statistic", JsonUtility.ToJson(new Stats())));
            PlayerDataController.playerStats =
                JsonUtility.FromJson<PlayerStats>(PlayerPrefs.GetString("PlayerStats",
                    JsonUtility.ToJson(new PlayerStats())));
            Setting.settings =
                JsonUtility.FromJson<MyPlayerSettings>(PlayerPrefs.GetString("Settings",
                    JsonUtility.ToJson(new MyPlayerSettings())));
            FieldManager.fields =
                JsonUtility.FromJson<Fields>(PlayerPrefs.GetString("Fields", JsonUtility.ToJson(new Fields())));
            UnlockCircles.upgrade = JsonUtility.FromJson<UpgradeCircle>(
                PlayerPrefs.GetString("UpgradeCircle", JsonUtility.ToJson(new UpgradeCircle())));
            BallsManager.balls = JsonUtility.FromJson<StatsBall>(
                PlayerPrefs.GetString("BallsManager", JsonUtility.ToJson(new StatsBall())));
            ChallengeManager.progress = JsonUtility.FromJson<ChallengeProgress>(
                PlayerPrefs.GetString("Challenge", JsonUtility.ToJson(new ChallengeProgress())));
            SkinShopController.skins = JsonUtility.FromJson<Skins>(
                PlayerPrefs.GetString("Skins", JsonUtility.ToJson(new Skins())));
            instance.StartCoroutine(instance.loadQuest());

            Debug.Log("Player data complete loading.");
            isLoading = true;
        }

        private void LoadQuest()
        {
            if (!QuestManager.instance) return;
            QuestManager.progress = new Quest[10];
            QuestManager.progress[0] = JsonUtility.FromJson<Quest>(
                PlayerPrefs.GetString("GlobalQuest",
                    JsonUtility.ToJson(new Quest(QuestManager.instance.completeGlobalEvent.Length)))
            );
            for (int _i = 0; _i < 9; _i++)
            {
                QuestManager.progress[_i + 1] = JsonUtility.FromJson<Quest>(
                    PlayerPrefs.GetString("Quest" + _i,
                        JsonUtility.ToJson(new Quest(QuestManager.instance.completeLocalEvent.Length)))
                );
            }

            QuestManager.instance.InitializeQuest();
        }

        private IEnumerator loadQuest()
        {
            while (!QuestManager.instance)
            {
                yield return null;
            }

            LoadQuest();
        }
    }
}