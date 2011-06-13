using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class GamerManager
    {
        static List<GamerEntity> gamerEntities = new List<GamerEntity>();
        static List<Entity2D> playerEntities = new List<Entity2D>();

        public static List<GamerEntity> getGamerEntities() { return gamerEntities; }
        public static List<Entity2D> getPlayerEntities() { return playerEntities; }
        public static GamerEntity getSessionOwner()
        {
            foreach (GamerEntity ge in gamerEntities)
            {
                if (ge.SessionOwner)
                {
                    return ge;
                }
            }
            return null;
        }
        public static Player getMainPlayer()
        {
            foreach (GamerEntity ge in gamerEntities)
            {
                if (ge.SessionOwner)
                {
                    return ge.Player;
                }
            }
            return null;
        }
        public static GamerEntity getGamerEntity(PlayerIndex playerIndex)
        {
            foreach(GamerEntity ge in gamerEntities)
            {
                if (ge.PlayerIndex == playerIndex)
                {
                    return ge;
                }
            }
            return null;
        }
        public static ControlPad getMainControls()
        {
            // TODO apaño
            if (getSessionOwner() == null)
                return ControlPadManager.Instance.controlPads[(int)PlayerIndex.One];

            return getSessionOwner().Controls;
        }

        public static bool isTrial()
        {
            return Guide.IsTrialMode;
        }
        public static void updateTrialMessage()
        {
            if (!Guide.IsTrialMode)
                return;
            if (GamerManager.getMainControls().X_firstPressed())
            {
                //TASKGamerManager.buyGame();
            }
        }
        public static void renderTrialMessage(Vector2 positionFromCenter, float scale)
        {
            if (!Guide.IsTrialMode)
                return;
            TextKey.BuyGame.Translate().render(
                Screen.getXYfromCenter(positionFromCenter), scale, Color.LightBlue, StringManager.tTextAlignment.Centered,
                SB.font, 1000, 0, Color.Blue, 1.0f, new Vector2(1, 1), StringManager.tStyle.Border);
        }

        public static GamerEntity createGamerEntity(PlayerIndex playerIndex, bool sessionOwner)
        {
            GamerEntity ge = new GamerEntity(sessionOwner);
            if (Gamer.SignedInGamers[playerIndex] != null)
            {
                ge.Gamer = Gamer.SignedInGamers[playerIndex];
            }
            gamerEntities.Add(ge);
            createPlayer(ge);
            ge.Controls = ControlPadManager.Instance.controlPads[(int)playerIndex];
            ge.PlayerIndex = playerIndex;
            playerEntities.Add(ge.Player);
            return ge;
        }

        public static void createPlayer(GamerEntity ge)
        {
            ge.Player = new Player(ge, "player", Vector3.Zero, 0);
        }

        public static void updatePlayers()
        {
            foreach (GamerEntity g in gamerEntities)
            {
                g.updatePlayer();
            }
        }
    }
}
