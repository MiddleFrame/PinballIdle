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

    [SerializeField]
    private Button _close;
    [SerializeField]
    private Button _discord;

    private void Start()
    {
        _close.onClick.AddListener(Close);
        _discord.onClick.AddListener(()=>Application.OpenURL(Setting.DISCORD_URL));
    }

    private void OnDestroy()
    {
        _close.onClick.RemoveListener(Close);
        _discord.onClick.RemoveListener(()=>Application.OpenURL(Setting.DISCORD_URL));
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        _pointSpentText.text = stats.pointSpent.ToString();
        _questCompletedText.text = stats.questCompleted.ToString();
        _maxPointText.text = stats.maxPoint.ToString();
        _countOfBallsText.text = stats.countOfBalls.ToString();
        _lostBallsText.text = stats.lostBalls.ToString();
        gameObject.SetActive(true);
    }

}


public class Stats
{
    public long pointSpent =0;
    public int questCompleted = 0;
    public int maxPoint = 0;
    public int countOfBalls = 0;
    public int lostBalls = 0;
}