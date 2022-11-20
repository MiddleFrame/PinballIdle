using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _firstWindow;
    [SerializeField]
    private GameObject _rankWindow;
    private static GameObject rankWindow;
    private bool _isNeedTutorial
    {
        get => bool.Parse(PlayerPrefs.GetString("isNeedTutorial", true.ToString()));
        set => PlayerPrefs.SetString("isNeedTutorial", value.ToString());
    }
    public static bool _isNeedTutorialRank
    {
        get => bool.Parse(PlayerPrefs.GetString("isNeedTutorialRank", true.ToString()));
        set => PlayerPrefs.SetString("isNeedTutorialRank", value.ToString());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (_isNeedTutorial)
        {
            _isNeedTutorial = false;
            _firstWindow.SetActive(true);
        }

        rankWindow = _rankWindow;
    }

    public static void RankTutorialWindow()
    {
        rankWindow.SetActive(true);
        _isNeedTutorialRank = false;
    }

}
