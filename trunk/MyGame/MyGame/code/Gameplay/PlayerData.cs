﻿using System;
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
        public int totalXP;
        public int XP;
        public int lifeOrbs;
        public int wishOrbs;
        public int petOrbs;

        public Dictionary<string, PlayerSkill> skills = new Dictionary<string, PlayerSkill>();
        public Dictionary<string, bool> levelsPassed = new Dictionary<string, bool>();

        public void initLevelsPassed()
        {
            levelsPassed["final_Level01"] = true;
            levelsPassed["onionVillage"] = true;
            levelsPassed["epilepticMacedonia"] = false;
            levelsPassed["level2"] = false;
        }
        // initialize the tree with the costs and links between skills
        public void initSkills()
        {
            skills["dash1"] = new PlayerSkill("Dash", 350);
            skills["dash2"] = new PlayerSkill("Super Dash", 1000);
            skills["dash3"] = new PlayerSkill("Mega Dash", 3000);
            skills["plasma"] = new PlayerSkill("Plasma", 1500);
            skills["powerShot"] = new PlayerSkill("Power Shot", 500);
            skills["life1"] = new PlayerSkill("Life", 500);

            XP = 10000;
            foreach (PlayerSkill ps in skills.Values)
            {
                //ps.obtained = true;
            }
        }
        public void initNewData()
        {
            XP = 0;
            lifeOrbs = 0;
            wishOrbs = 0;
            petOrbs = 0;
            initLevelsPassed();
            initSkills();
        }

        public void loadXMLAndPublishData()
        {
            initSkills();
        }
        public void saveDataToXML()
        {

        }

        // methods to pick the right const value
        public const float DASH1_COOLDOWN = 0.8f;
        public const float DASH2_COOLDOWN = 0.6f;
        public const float DASH3_COOLDOWN = 0.4f;
        public float getDashCooldown()
        {
            if (skills["dash3"].obtained)
            {
                return DASH3_COOLDOWN;
            }
            else if (skills["dash2"].obtained)
            {
                return DASH2_COOLDOWN;
            }
            else if (skills["dash1"].obtained)
            {
                return DASH1_COOLDOWN;
            }
            return 0.0f;
        }
    }
}