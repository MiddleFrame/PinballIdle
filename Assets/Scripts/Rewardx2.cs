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
        if (Advertisement.isSupported)
            Advertisement.Initialize("4265503", false);
      
    }

    
}
