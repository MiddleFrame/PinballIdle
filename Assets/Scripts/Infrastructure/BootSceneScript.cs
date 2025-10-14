using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSceneScript : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("MainFieldsScene");
    }
}