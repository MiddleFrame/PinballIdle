using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static int Quest0Challenge;
    public Text[] QuestsTexts;
    public Text[] ProgressQuestsTexts;
   static public Text[] StaticProgressQuestsTexts = new Text[10];
    static public bool[] QuestsCompleate = new bool[10] { false, false,true,true,true,true,true,true,true,true };
    // Start is called before the first frame update
    void Awake()
    {
        Quest0Challenge = 35 + 25 * Teleport.i;
        for (int i=0;i<ProgressQuestsTexts.Length;i++)
        StaticProgressQuestsTexts[i] = ProgressQuestsTexts[i];
        

    }

    static public void Updates()
    {

        if (GameManager.maximumPoint <= Quest0Challenge)
            StaticProgressQuestsTexts[0].text = $"{GameManager.maximumPoint}/{Quest0Challenge}";
        else
        {
            StaticProgressQuestsTexts[0].text = $"{Quest0Challenge}/{Quest0Challenge}";
            StaticProgressQuestsTexts[0].color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
            QuestsCompleate[0] = true;
        }
    }

    static public void Quest2()
    {
        if (Teleport.i == 0)
        {
            StaticProgressQuestsTexts[1].color = new Color32(0x39, 0xB5, 0x4A, 0xFF);
            StaticProgressQuestsTexts[1].text = "20/20";
            QuestsCompleate[1] = true;
        }
    }
}
