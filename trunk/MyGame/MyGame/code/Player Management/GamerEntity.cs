using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace MyGame
{
    public class GamerEntity
    {
        SignedInGamer gamer;
        Player player;
        PlayerIndex playerIndex;
        ControlPad controls = new ControlPad();
        public PlayerData data { get; set; }
        bool sessionOwner;

        public SignedInGamer Gamer
        {
            get { return gamer; }
            set { gamer = value; }
        }
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
            set { playerIndex = value; }
        }
        public ControlPad Controls
        {
            get { return controls; }
            set { controls = value; }
        }
        public bool SessionOwner
        {
            get { return sessionOwner; }
            set { sessionOwner = value; }
        }

        public GamerEntity(bool sessionOwner)
        {
            this.sessionOwner = sessionOwner;

            data = new PlayerData();
            data.initNewData();
        }

        public void updateInput()
        {
            controls.update(playerIndex);
        }

        public void updatePlayer()
        {
            player.update(controls);
        }
    }
}
