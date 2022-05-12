using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;
public class InitializeTheAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Yodo1AdBuildConfig config = new Yodo1AdBuildConfig().enableUserPrivacyDialog(true);
        Yodo1U3dMas.SetAdBuildConfig(config);
        Yodo1U3dMas.InitializeSdk();
    }

}
