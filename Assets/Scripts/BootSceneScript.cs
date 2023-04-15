using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSceneScript : MonoBehaviour
{
    private IEnumerator Start()
    {
        if (false)
        {
            yield return null;
        }
        SceneManager.LoadScene("MainFieldsScene");
    }

    
}
