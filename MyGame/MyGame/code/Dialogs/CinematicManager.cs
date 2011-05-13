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

        public void update()
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
                    eventsToUpdate.RemoveAt(i);
                    --i;
                }
            }
        }
    }

    class CinematicManager
    {
        static CinematicManager instance = null;
        CinematicManager()
        {
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

        public void initFakeCinematic()
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(1.0f, 50.0f, GamerManager.getMainPlayer());
            ae1.setAt(new Vector3(0.0f, -700.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, 0.0f, 0.0f), 50.0f);

            //DialogEvent de1 = new DialogEvent(2.0f, 2.0f, tDialogCharacter.Wish, "Vamos a por ellos!!!", 5.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            //cinematic.events.Add((CinematicEvent)de1);
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
            cinematicToPlay = cinematics[cinematic];
        }

        public void update()
        {
            if (cinematicToPlay != null)
            {
                cinematicToPlay.update();
            }
        }
        public void render()
        {
            if (cinematicToPlay != null)
            {
                cinematicToPlay.update();
            }
        }
    }
}
