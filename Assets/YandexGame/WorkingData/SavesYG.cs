
using Controllers;
using Managers;
using Shop;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";

        // Ваши сохранения
        public CostAndGrade defaultBuff = new();
        public StopperGrades buyStopper = new(9);
        public Stats statistics = new();
        public PlayerStats playerDataController= new();
        public MyPlayerSettings setting= new();
        public Fields fieldManager= new();
        public UpgradeCircle unlockCircles= new();
        public StatsBall challengeManager= new();
        public ChallengeProgress ballsManager= new();
        public Skins skinShopController= new();
    }
}
