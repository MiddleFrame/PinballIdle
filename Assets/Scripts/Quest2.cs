using UnityEngine;

public class Quest2 : MonoBehaviour
{
    public static bool leftRight=false;
    public static int Quest20 = 0;


    public void Quest20left(bool flag){
        if (leftRight == !flag)
        {
            Quest20++;
            if(!QuestManager.QuestsCompleate[1])
            QuestManager.StaticProgressQuestsTexts[1].text = $"{Quest20}/20";
            if (Quest20 >= 20)
            {
                QuestManager.Quest2();
            }
        }
        else
            Quest20 = 0;
        leftRight = flag;
    }
}
