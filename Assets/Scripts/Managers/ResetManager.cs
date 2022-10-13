using Controllers;
using Shop;
using UnityEngine;

namespace Managers
{
    public class ResetManager : MonoBehaviour
    {
        public void ResetCurrentField()
        {
            ResetProgressOnField(FieldManager.currentField);
        }
        private void ResetProgressOnField(int field)
        {
            DefaultBuff.grade.multiplyPoint[field] += 0.2f;
            if (DefaultBuff.grade.multiplyPoint[field] < 1.3f)
                DefaultBuff.grade.autoFlippers[field] = false;
            DefaultBuff.grade.bonusTime[field] = 30;
            DefaultBuff.grade.expTime[field] = 30;
            DefaultBuff.grade.pointOnBit[field] = 1;
            if (DefaultBuff.grade.multiplyPoint[field] < 1.5f)
                BuyStopper.grades.isStopper[field] = false;
            PlayerDataController.playerStats.lvl[field] = 1;
            PlayerDataController.playerStats.exp[field] = 0;
            //Тут мб оставлять N шаров
            ChallengeManager.progress.balls[field] = DefaultBuff.grade.multiplyPoint[field] < 1.7f?0:1;
            ChallengeManager.progress.countCompleteChallenge[field] = DefaultBuff.grade.multiplyPoint[field] < 1.7f?0:1;
            ChallengeManager.progress.currentProgressChallenge[field] = 0;
        }
    }
}