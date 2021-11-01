using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEditor;


public class Rewardx2 : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Obsolete]
    void Start()
    {
        if (!GameManager.premium)
        {
            if (Advertisement.isSupported)
                Advertisement.Initialize("4265503", false);
        }
    }

    
}

public static class VibratorWrapper
{

    static AndroidJavaObject vibrator = null;

    static VibratorWrapper()
    {

        var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var unityPlayerActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        vibrator = unityPlayerActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

    }
    public static bool HasVibrator()
    {
        return vibrator.Call<bool>("hasVibrator");
    }

    public static void Cancel()
    {
        if (HasVibrator()) vibrator.Call("cancel");
    }
    public static void Vibrate(long time)
    {
        
        if (HasVibrator()) vibrator.Call("vibrate", time);
    }
}