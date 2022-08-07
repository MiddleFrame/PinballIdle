using UnityEngine;
using UnityEngine.UI;
using YG;

public class SaverTest : MonoBehaviour
{
    [SerializeField] InputField integerText;
    [SerializeField] InputField stringifyText;
    [SerializeField] Text systemSavesText;
    [SerializeField] Toggle[] booleanArrayToggle;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    public void Save()
    {
       
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        
    }
}
