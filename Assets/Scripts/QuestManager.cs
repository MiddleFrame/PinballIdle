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
    static public bool[] QuestsCompleate = new bool[10] { false, false,false,false,false, false, false, false, false,false };
    // Start is called before the first frame update
    void Awake()
    {
       
        for (int i=0;i<ProgressQuestsTexts.Length;i++)
        StaticProgressQuestsTexts[i] = ProgressQuestsTexts[i];
        

    }

   
}
