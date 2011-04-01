using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    class PlayerSkill
    {
        public string name;
        public int cost;
        public bool obtained;
        public string preSkill;

        public PlayerSkill(string name, int cost, string preSkill = null, bool obtained = false)
        {
            this.name = name;
            this.cost = cost;
            this.preSkill = preSkill;
            this.obtained = obtained;
        }
    }

    class PlayerData
    {
        public const float DASH_COOLDOWN = 0.8f;

        public int XP;
        public int lifeOrbs;
        public int wishOrbs;
        public int petOrbs;

        public Dictionary<string, PlayerSkill> skills = new Dictionary<string, PlayerSkill>();

        // initialize the tree with the costs and links between skills
        public void initSkills()
        {
            skills["dash"] = new PlayerSkill("Dash", 100);
            skills["powerShot"] = new PlayerSkill("Power Shot", 300);
            skills["doublePowerShot"] = new PlayerSkill("Double Power Shot", 300, "powerShot");

            foreach (PlayerSkill ps in skills.Values)
            {
                ps.obtained = true;
            }
        }
        public void initNewData()
        {
            XP = 0;
            lifeOrbs = 0;
            wishOrbs = 0;
            petOrbs = 0;
            initSkills();
        }

        public void loadXMLAndPublishData()
        {
            initSkills();
        }
        public void saveDataToXML()
        {

        }
    }
}
