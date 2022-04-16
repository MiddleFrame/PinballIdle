using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private UnlockCircles[] unlockCircles;
    private void OnApplicationFocus(bool focus)
    {
        OnApplicationPause(!focus);

    }
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



    void SaveGame()
    {
        PlayerPrefs.SetString("StandartBuff", JsonUtility.ToJson(StandartBuff.grade));
        PlayerPrefs.SetString("StoppersBuff", JsonUtility.ToJson(BuyStopper.grades));
        PlayerPrefs.SetString("Statistic", JsonUtility.ToJson(Statistics.stats));
        PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(PlayerDataController.playerStats));
        PlayerPrefs.SetString("Settings", JsonUtility.ToJson(Setting.settings));
        PlayerPrefs.SetString("Moneybox", JsonUtility.ToJson(PigMoneybox.grades));
        for (int i = 0; i < unlockCircles.Length; i++)
        {
            PlayerPrefs.SetString($"UpgradeCircle {unlockCircles[i].field}", JsonUtility.ToJson(unlockCircles[i].upgrade));
        }
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    void LoadGame()
    {
        StandartBuff.grade = JsonUtility.FromJson<CostAndGrade>(PlayerPrefs.GetString("StandartBuff", JsonUtility.ToJson(new CostAndGrade())));
        BuyStopper.grades = JsonUtility.FromJson<StopperGrades>(PlayerPrefs.GetString("StoppersBuff", JsonUtility.ToJson(new StopperGrades(16))));
        Statistics.stats = JsonUtility.FromJson<Stats>(PlayerPrefs.GetString("Statistic", JsonUtility.ToJson(new Stats())));
        PlayerDataController.playerStats = JsonUtility.FromJson<PlayerStats>(PlayerPrefs.GetString("PlayerStats", JsonUtility.ToJson(new PlayerStats())));
        Setting.settings = JsonUtility.FromJson<PlayerSettings>(PlayerPrefs.GetString("Settings", JsonUtility.ToJson(new PlayerSettings())));
        PigMoneybox.grades = JsonUtility.FromJson<AfkGrade>(PlayerPrefs.GetString("Moneybox", JsonUtility.ToJson(new AfkGrade())));
        for (int i = 0; i < unlockCircles.Length; i++)
        {
        unlockCircles[i].upgrade = JsonUtility.FromJson<UpgradeCircle>(PlayerPrefs.GetString($"UpgradeCircle {unlockCircles[i].field}", JsonUtility.ToJson(new UpgradeCircle(unlockCircles[i].circles.Length))));
            if(unlockCircles[i].upgrade == null)
            {
                unlockCircles[i].upgrade = new UpgradeCircle(unlockCircles[i].circles.Length);
            }
        }

    }
}
