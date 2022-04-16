using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{

    public static PlayerStats playerStats;
    private static PlayerDataController instance;
    [SerializeField]
    private GameObject _lvlupPanel;
    [SerializeField]
    private Text _gems;
    public static int Gems
    {
        get { return playerStats.gems; }
        set
        {
            playerStats.gems = value;
            instance._gems.text = GameManager.NormalSum(playerStats.gems);
        }
    }
    [SerializeField]
    private Image _exp;
    public static int Exp
    {
        get { return playerStats.exp; }
        set
        {
            playerStats.exp = value;
            instance._exp.fillAmount = (float)value / (200f * Lvl);

            if (instance._exp.fillAmount >= 1f)
            {
                if (Lvl > 0)
                {
                    Lvl++;
                    instance._lvlOnLvlupPanel.text = Lvl.ToString();
                    instance._lvlOnLvlupPanelMultiply.text = "x"+Lvl.ToString();
                    instance._lvlupPanel.SetActive(true);
                }
                playerStats.exp = 0;
                instance._exp.fillAmount = 0;
            }
        }
    }
    [SerializeField]
    private Text _lvlMain;
    [SerializeField]
    private Text _lvlOnLvlupPanel;
    [SerializeField]
    private Text _lvlOnLvlupPanelMultiply;
    [SerializeField]
    private Text _lvlbuff;
    public static int Lvl
    {
        get { return playerStats.lvl; }
        set
        {
            playerStats.lvl = value;
            instance._lvlMain.text = playerStats.lvl.ToString();
            instance._lvlbuff.text = $"x {playerStats.lvl*InterstitialAndReward.hitMultiply}";
        }
    }
    [SerializeField]
    private Text _pointSumText;
    static public long PointSum
    {
        get { return playerStats.pointSum; }
        set
        {
            playerStats.pointSum = value;
            instance._pointSumText.text = GameManager.NormalSum(playerStats.pointSum);
        }
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PointSum = playerStats.pointSum;
        Lvl = playerStats.lvl;
        Exp = playerStats.exp;
        Gems = playerStats.gems;
    }
}



public class PlayerStats
{
    public long pointSum;
    public int gems;
    public int lvl =1;
    public int exp;
}