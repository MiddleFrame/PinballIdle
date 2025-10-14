using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PopupsButtonView : MonoBehaviour
{
    [SerializeField]
    private Button _setting;

    [SerializeField]
    private Button _rank;

    [SerializeField]
    private Button _quest;

    [Inject]
    private PopupsController _popupsController;

    private void OnEnable()
    {
        _setting.onClick.AddListener(()=>_popupsController.ShowPopup<SettingsPanel>());
        _rank.onClick.AddListener(()=>_popupsController.ShowPopup<RankPanel>());
        _quest.onClick.AddListener(()=>_popupsController.ShowPopup<QuestPanel>());
    }

    private void OnDisable()
    {
        _setting.onClick.RemoveListener(()=>_popupsController.ShowPopup<SettingsPanel>());
        _rank.onClick.RemoveListener(()=>_popupsController.ShowPopup<RankPanel>());
        _quest.onClick.RemoveListener(()=>_popupsController.ShowPopup<QuestPanel>());

    }
}
