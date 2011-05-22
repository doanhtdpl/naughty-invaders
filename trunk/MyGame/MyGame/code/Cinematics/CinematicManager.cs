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
                if (!eventToUpdate.update())
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

        public void initFakeCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(GamerManager.getMainPlayer(), 12.0f, 0.0f);
            ae1.setAt(new Vector3(0.0f, -700.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, 0.0f, 0.0f), 200.0f);

            DialogEvent de1 = new DialogEvent(tDialogCharacter.Wish, "Vamos a por pepinillos que tengo hambre porque he comido arroz con leche!!!", 70.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            cinematics["fakeCinematic"] = cinematic;
        }

        public string getCharacterName(tDialogCharacter character)
        {
            switch (character)
            {
                case tDialogCharacter.Wish: return "Wish";
                case tDialogCharacter.OnionElder: return "Onion Elder";
                case tDialogCharacter.KingTomato: return "King Tomato";
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
