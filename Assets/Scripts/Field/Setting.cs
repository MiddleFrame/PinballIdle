public static class Setting
{
    public const string DISCORD_URL = "https://discord.gg/5YMYWCcsAv";
    private static MyPlayerSettings settings;

    public static bool ExNum
    {
        get => settings.exNum;
        set => settings.exNum = value;
    }

    public static bool Sound
    {
        get => settings.sound;
        set => settings.sound = value;
    }

    public static bool LevelText
    {
        get => settings.levelText;
        set => settings.levelText = value;
    }

    public static bool Vibration
    {
        get => settings.vibration;
        set => settings.vibration = value;
    }

    public static MyPlayerSettings GetAllSettings => settings;

    public static void Setup(MyPlayerSettings initSettings)
    {
        settings = initSettings;
    }


    public static void Init(SettingsPanel settingsPanel)
    {
        if (!settings.exNum)
        {
            settings.exNum = !settings.exNum;
            settingsPanel.ChangeNum();
        }

        if (!settings.sound)
        {
            settings.sound = !settings.sound;
            settingsPanel.ChangeSound();
        }

        if (!settings.levelText)
        {
            settings.levelText = !settings.levelText;
            settingsPanel.ViewLevelText();
        }

        if (!settings.vibration)
        {
            settings.vibration = !settings.vibration;
            settingsPanel.ChangeVibration();
        }
    }
}


public class MyPlayerSettings
{
    public bool exNum = true;
    public bool sound = true;
    public bool levelText = true;
    public bool vibration = true;
}