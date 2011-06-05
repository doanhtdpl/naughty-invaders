using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class Cinematic
    {
        public List<CinematicEvent> events = new List<CinematicEvent>();
        CinematicEvent eventToUpdate = null;

        // returns false when the cinematic ends
        public bool update()
        {
            // see if the next event needs to be started
            if (eventToUpdate != null)
            {
                if (!eventToUpdate.update(GamerManager.getMainControls().A_firstPressed()))
                {
                    eventToUpdate.endEvent();
                    eventToUpdate = null;
                }
            }
            else if (events.Count > 0)
            {
                events[0].activationTime -= SB.dt;
                if (events[0].activationTime < 0.0f)
                {
                    eventToUpdate = events[0];
                    eventToUpdate.startEvent();
                    events.RemoveAt(0);
                }
            }

            return events.Count > 0 || eventToUpdate != null;
        }

        public void skipWholeCinematic()
        {
            while (eventToUpdate != null)
            {
                eventToUpdate.startEvent();
                if (eventToUpdate.update(true, true))
                {
                    eventToUpdate.update(true, true);
                }
                eventToUpdate.endEvent();

                if (events.Count > 0)
                {
                    events.RemoveAt(0);
                    if (events.Count > 0)
                    {
                        eventToUpdate = events[0];
                    }
                }
                else
                {
                    eventToUpdate = null;
                }
            }
        }

        public void render()
        {
            if (eventToUpdate != null)
            {
                eventToUpdate.render();
            }
        }
    }

    class CinematicManager
    {
        static CinematicManager instance = null;
        CinematicManager()
        {
            DialogEvent.initialize();
            cinematicToPlay = null;
        }
        public static CinematicManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CinematicManager();
                }
                return instance;
            }
        }

        // have stored from the beginning all of the cinematics of each level
        Dictionary<string, Cinematic> cinematics = new Dictionary<string, Cinematic>();
        public Cinematic cinematicToPlay { get; set; }

        public void addCinematic(string name, Cinematic cinematic)
        {
            cinematics[name] = cinematic;
        }

        public string getCharacterName(tDialogCharacter character)
        {
            switch (character)
            {
                case tDialogCharacter.Wish: return "Wish";
                case tDialogCharacter.OnionElder: return "Onion Elder";
                case tDialogCharacter.KingTomato: return "King Tomato";
                case tDialogCharacter.Macedonia: return "Macedonia";
            }
            return "";
        }

        public void playCinematic(string cinematic)
        {
            setUpdatableOnPlayersAndEnemies(false);
            cinematicToPlay = cinematics[cinematic];
        }
        public void setUpdatableOnPlayersAndEnemies(bool update)
        {
            RenderableEntity2D.tUpdateState updateState = update ? RenderableEntity2D.tUpdateState.Update : RenderableEntity2D.tUpdateState.NoUpdate;

            List<GamerEntity> players = GamerManager.getGamerEntities();
            for (int i = 0; i < players.Count; ++i)
            {
                players[i].Player.updateState = updateState;
            }

            List<Entity2D> enemies = EnemyManager.Instance.getEnemies();
            for (int i = 0; i < enemies.Count; ++i)
            {
                ((RenderableEntity2D)enemies[i]).updateState = updateState;
            }
        }

        public void update()
        {
            if (cinematicToPlay != null)
            {
#if DEBUG
                if (GamerManager.getMainControls().Y_firstPressed())
                {
                    cinematicToPlay.skipWholeCinematic();
                    setUpdatableOnPlayersAndEnemies(true);
                    cinematicToPlay = null;
                    return;
                }
#endif
                if (!cinematicToPlay.update())
                {
                    setUpdatableOnPlayersAndEnemies(true);
                    cinematicToPlay = null;
                }
                
            }
        }
        public void render()
        {
            if (cinematicToPlay != null)
            {
                cinematicToPlay.render();
            }
        }
    }
}
