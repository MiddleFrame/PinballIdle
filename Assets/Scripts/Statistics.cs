using System;
using UnityEngine;
using UnityEngine.UI;
public class Statistics : MonoBehaviour
{
    public static Stats stats;
    [SerializeField]
    Text _pointSpentText;
    [SerializeField]
    Text _questCompletedText;
    [SerializeField]
    Text _maxPointText;
    [SerializeField]
    Text _countOfBallsText;
    [SerializeField]
    Text _lostBallsText;

    private void OnEnable()
    {
        _pointSpentText.text = stats.pointSpent.ToString();
        _questCompletedText.text = stats.questCompleted.ToString();
        _maxPointText.text = stats.maxPoint.ToString();
        _countOfBallsText.text = stats.countOfBalls.ToString();
        _lostBallsText.text = stats.lostBalls.ToString();
    }

}

[Serializable]
public class Stats
{
    public long pointSpent =0;
    public int questCompleted = 0;
    public int maxPoint = 0;
    public int countOfBalls = 0;
    public int lostBalls = 0;
}