namespace MyGame
{
    public class SaveData
    {
        public string gamertag;
        public float musicLevel;
        public float soundLevel;
        public int rumble;

        // achievements
        public bool passed15 = false;
        public bool passed30 = false;
        public bool passedAll = false;
        public bool allBronze = false;
        public bool allSilver = false;
        public bool allGold = false;
        public bool superScore = false;

        public void reset()
        {
            musicLevel = 70;
            soundLevel = 80;
            rumble = 1;
        }
    }
}