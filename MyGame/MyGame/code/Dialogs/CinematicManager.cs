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
        public float timer { get; set; }

        public List<CinematicEvent> events = new List<CinematicEvent>();
        public List<CinematicEvent> eventsToUpdate = new List<CinematicEvent>();

        // returns false when the cinematic ends
        public bool update()
        {
            timer += SB.dt;

            // see which events needs to be started
            for (int i = 0; i < events.Count; ++i)
            {
                if (events[i].state == CinematicEvent.tState.Waiting && timer > events[i].activationTime)
                {
                    eventsToUpdate.Add(events[i]);
                    events.RemoveAt(i);
                    --i;
                }
            }

            // update all started events
            for (int i = 0; i < eventsToUpdate.Count; ++i)
            {
                if (!eventsToUpdate[i].update())
                {
                    eventsToUpdate[i].endEvent();
                    eventsToUpdate.RemoveAt(i);
                    --i;
                }
            }

            return events.Count + eventsToUpdate.Count > 0;
        }

        public void render()
        {
            for (int i = 0; i < eventsToUpdate.Count; ++i)
            {
                eventsToUpdate[i].render();
            }
        }
    }

    class CinematicManager
    {
        static CinematicManager instance = null;
        CinematicManager()
        {
            DialogEvent.initialize();
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
        Cinematic cinematicToPlay = null;

        public void addCinematic(string name, Cinematic cinematic)
        {
            cinematics[name] = cinematic;
        }

        public void initFakeCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(0.0f, 12.0f, GamerManager.getMainPlayer());
            ae1.setAt(new Vector3(0.0f, -700.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, 0.0f, 0.0f), 200.0f);

            DialogEvent de1 = new DialogEvent(2.0f, 4.0f, tDialogCharacter.Wish, "Vamos a por pepinillos que tengo hambre porque he comido arroz con leche!!!", 70.0f);

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
