using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    struct PlayerSkill
    {
        public string name;
        public int cost;
        public bool obtained;

        public PlayerSkill(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
            this.obtained = false;
        }
    }

    class PlayerData
    {
        int XP;

        Network<PlayerSkill> skillTree = new Network<PlayerSkill>();

        public Network<PlayerSkill> getSkillTree()
        {
            return skillTree;
        }
    }
}
