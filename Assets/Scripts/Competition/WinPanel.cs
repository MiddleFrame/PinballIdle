using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Competition
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _place;

        [SerializeField]
        private Text _time;

        [SerializeField]
        private Image _cup;

        [SerializeField]
        private Color[] _cupColor;
        [SerializeField]
        private GameObject _loseButton;

        [SerializeField]
        private GameObject _winText;

        [SerializeField]
        private GameObject _winButtons;

        [SerializeField]
        private int field;

        [Space(20), Header("Player"),SerializeField]
        private Text _reward;
        [SerializeField]
        private Text _buttonReward;
        [SerializeField]
        private Text _buttonX2reward;

        [SerializeField]
        private Text _congratulation;

        private void OnEnable()
        {
            _place.text = $"{CompetitionManager.winner} PLACE";
            _time.text = Timer.time.ToString("mm:ss");
            if (field == 0)
            {
                _loseButton.SetActive(CompetitionManager.winner > 3);
                _winButtons.SetActive(CompetitionManager.winner <= 3);
                _winText.SetActive(CompetitionManager.winner <= 3);
                _congratulation.text = CompetitionManager.winner <= 3 ? "Congratulation!" : "Better luck next time.";
                switch (CompetitionManager.winner)
                {
                    case 1:
                        _reward.text = "100";
                        _buttonReward.text = "100";
                        _buttonX2reward.text = "200";
                        AnalyticManager.FirstPlaceCompetition();
                        break;
                    case 2:

                        _reward.text = "75";
                        _buttonReward.text = "75";
                        _buttonX2reward.text = "150";
                        AnalyticManager.SecondPlaceCompetition();
                        break;
                    case 3:

                        _reward.text = "50";
                        _buttonReward.text = "50";
                        _buttonX2reward.text = "100";
                        AnalyticManager.ThirdPlaceCompetition();
                        break;
                    default:
                        AnalyticManager.LoseCompetition();
                        break;
                }
            }
            else
            {
                switch (CompetitionManager.winner)
                {
                    case 1:
                        _cup.color = _cupColor[0];
                        break;
                    case 2:

                        _cup.color = _cupColor[1];
                        break;
                    case 3:

                        _cup.color = _cupColor[2];
                        break;
                    default:
                        _cup.gameObject.SetActive(false);
                        break;
                }
            }
        }
    }
}